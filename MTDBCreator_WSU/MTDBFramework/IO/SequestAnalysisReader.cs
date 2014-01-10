﻿#region Namespaces

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using MTDBFramework.Algorithms.RetentionTimePrediction;
using MTDBFramework.Data;
using MTDBFramework.Database;

#endregion

namespace MTDBFramework.IO
{
    public class SequestAnalysisReader : TableDataReaderBase<SequestResult>, IAnalysisReader
    {
        public Options ReaderOptions { get; set; }

        public SequestAnalysisReader(Options options)
        {
            this.ReaderOptions = options;
        }

        public LcmsDataSet Read(string path)
        {
            List<SequestResult> results = new List<SequestResult>();
            SequestTargetFilter filter = new SequestTargetFilter(this.ReaderOptions);

            // Get Result to Sequence Map

            ResultToSequenceMapReader resultToSequenceMapReader = new ResultToSequenceMapReader();
            Dictionary<int, int> resultToSequenceDictionary = new Dictionary<int, int>();

            foreach (ResultToSequenceMap map in resultToSequenceMapReader.Read(path.Insert(path.LastIndexOf(".txt"), "_ResultToSeqMap")))
            {
                resultToSequenceDictionary.Add(map.ResultId, map.UniqueSequenceId);
            }

            // Get Sequence Info

            SequenceInfoReader sequenceInfoReader = new SequenceInfoReader();
            Dictionary<int, SequenceInfo> sequenceInfoDictionary = new Dictionary<int, SequenceInfo>();

            foreach (SequenceInfo info in sequenceInfoReader.Read(path.Insert(path.LastIndexOf(".txt"), "_SeqInfo")))
            {
                sequenceInfoDictionary.Add(info.Id, info);
            }

            // Get Targets

            using (StreamReader reader = new StreamReader(path))
            {
                this.SetHeaderIndices(reader.ReadLine());

                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    SequestResult result = this.ProcessLine(line);

                    if (!filter.ShouldFilter(result))
                    {
                        // Database use
                        result.DataSet = new TargetDataSet() { Path = path };

                        if (resultToSequenceDictionary.ContainsKey(result.AnalysisId))
                        {
                            result.IsSeqInfoExist = 1;
                            result.ModificationCount = sequenceInfoDictionary[resultToSequenceDictionary[result.AnalysisId]].ModificationCount;
                            result.ModificationDescription = sequenceInfoDictionary[resultToSequenceDictionary[result.AnalysisId]].ModificationDescription;
                            result.SeqInfoMonoisotopicMass = sequenceInfoDictionary[resultToSequenceDictionary[result.AnalysisId]].MonoisotopicMass;
                        }

                        results.Add(result);
                    }
                }
            }

            AnalysisReaderHelper.CalculateObservedNet(results);
            AnalysisReaderHelper.CalculatePredictedNet(RetentionTimePredictorFactory.CreatePredictor(this.ReaderOptions.PredictorType), results);

            return new LcmsDataSet(Path.GetFileNameWithoutExtension(path), LcmsIdentificationTool.Sequest, results);
        }

        protected override void SetHeaderIndices(string actualHeader)
        {
            DefaultHeaders header;

            string[] actualHeaders = actualHeader.Split(this.Delimiters, StringSplitOptions.None);

            for (int i = 0; i < actualHeaders.Length; i++)
            {
                bool result = Enum.TryParse(actualHeaders[i], true, out header);

                if (result)
                {
                    actualHeaderMaps.Add(header, i);
                }
            }
        }

        protected override SequestResult ProcessLine(string line)
        {
            string[] lineCells = line.Split(this.Delimiters, StringSplitOptions.None);

            SequestResult result = new SequestResult();

            // Fields in Target

            result.AnalysisId = Convert.ToInt32(lineCells[actualHeaderMaps[DefaultHeaders.HitNum]]);
            result.Scan = Convert.ToInt32(lineCells[actualHeaderMaps[DefaultHeaders.ScanNum]]);
            result.Charge = Convert.ToInt16(lineCells[actualHeaderMaps[DefaultHeaders.ChargeState]]);
            result.MonoisotopicMass = Convert.ToDouble(lineCells[actualHeaderMaps[DefaultHeaders.MH]]);
            result.MultiProteinCount = Convert.ToInt16(lineCells[actualHeaderMaps[DefaultHeaders.MultiProtein]]);
            result.Sequence = lineCells[actualHeaderMaps[DefaultHeaders.Peptide]];
            result.CleanPeptide = Target.CleanSequence(result.Sequence);
            result.Mz = result.MonoisotopicMass / result.Charge;
            result.PeptideInfo = new TargetPeptideInfo()
            {
                PeptideInfoCleanPeptide = result.CleanPeptide,
                PeptideInfoSequence = result.Sequence
            };
             // Fields in SequestResult

            result.ScanCount = Convert.ToInt16(lineCells[actualHeaderMaps[DefaultHeaders.ScanCount]]);
            result.XCorr = Convert.ToDouble(lineCells[actualHeaderMaps[DefaultHeaders.XCorr]]);
            result.DelCn = Convert.ToDouble(lineCells[actualHeaderMaps[DefaultHeaders.DelCn]]);
            result.Sp = Convert.ToDouble(lineCells[actualHeaderMaps[DefaultHeaders.Sp]]);
            result.Reference = lineCells[actualHeaderMaps[DefaultHeaders.Reference]];
            result.DelCn2 = Convert.ToDouble(lineCells[actualHeaderMaps[DefaultHeaders.DelCn2]]);
            result.RankSp = Convert.ToInt16(lineCells[actualHeaderMaps[DefaultHeaders.RankSp]]);
            result.RankXc = Convert.ToInt16(lineCells[actualHeaderMaps[DefaultHeaders.RankXc]]);
            result.DelM = Convert.ToDouble(lineCells[actualHeaderMaps[DefaultHeaders.DelM]]);
            result.XcRatio = Convert.ToDouble(lineCells[actualHeaderMaps[DefaultHeaders.XcRatio]]);
            result.PassFilt = Convert.ToBoolean(Convert.ToInt16(lineCells[actualHeaderMaps[DefaultHeaders.PassFilt]]));
            result.MScore = Convert.ToDouble(lineCells[actualHeaderMaps[DefaultHeaders.MScore]]);
            result.NumTrypticEnds = Convert.ToInt16(lineCells[actualHeaderMaps[DefaultHeaders.NumTrypticEnds]]);

            result.FScore = SequestResult.CalculatePeptideProphetDistriminantScore(result);

            return result;
        }

        private readonly Dictionary<DefaultHeaders, int> actualHeaderMaps = new Dictionary<DefaultHeaders, int>();

        private enum DefaultHeaders
        {
            HitNum,
            ScanNum,
            ScanCount,
            ChargeState,
            MH,
            XCorr,
            DelCn,
            Sp,
            Reference,
            MultiProtein,
            Peptide,
            DelCn2,
            RankSp,
            RankXc,
            DelM,
            XcRatio,
            PassFilt,
            MScore,
            NumTrypticEnds
        };
    }
}