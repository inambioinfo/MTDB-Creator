﻿#region Namespaces

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MTDBFramework.Data;
using MTDBFramework.UI;

#endregion

namespace MTDBFramework.IO
{
    public class AnalysisJobProcessor : IProcessor
    {
        private int mCurrentItem;
        private int mTotalItems;
        private AnalysisJobItem mCurrentJob;
        private bool mAbortRequested;

        private PHRPReaderBase mAnalysisReader;

        public Options ProcessorOptions { get; set; }

        public void AbortProcessing()
        {
            mAbortRequested = true;
            mAnalysisReader.AbortProcessing();
        }

        public AnalysisJobProcessor(Options options)
        {
            ProcessorOptions = options;
        }

		// Entry point for processing analysis job items. Accepts a IEnumerable of Analysis Job Items
		// and returns the same.
		// It will Analyse each one individually depending on the file type using PHRP Reader
        public IEnumerable<AnalysisJobItem> Process(IEnumerable<AnalysisJobItem> analysisJobItems, BackgroundWorker bWorker)
        {
            // analysisJobItems should have LcmsDataSet field be null
            mAbortRequested = false;

            mCurrentItem = 0;
            mTotalItems = analysisJobItems.Count();

            foreach (var jobItem in analysisJobItems)
            {
                if (bWorker.CancellationPending || mAbortRequested)
                    break;
                
                OnProgressChanged(new MtdbProgressChangedEventArgs(mCurrentItem, mTotalItems, jobItem));
                mCurrentJob = jobItem;

                mAnalysisReader = PhrpReaderFactory.Create(jobItem.FilePath, ProcessorOptions);

                mAnalysisReader.ProgressChanged += analysisReader_ProgressChanged;

                // Reads the jobItem using the reader returned by the Reader Factory
                jobItem.DataSet = mAnalysisReader.Read(jobItem.FilePath);

                mCurrentItem++;
                
            }
            
            return analysisJobItems;
        }

        private void analysisReader_ProgressChanged(object sender, PercentCompleteEventArgs e)
        {
            float percentComplete = (mCurrentItem * 100 + e.PercentComplete) / (mTotalItems * 100);

            var effectiveItemCount = (int)(mCurrentItem * 100 + e.PercentComplete);

            OnProgressChanged(new MtdbProgressChangedEventArgs(effectiveItemCount, mTotalItems * 100, e.CurrentTask, mCurrentJob));
        }

        #region Events

        public event MtdbProgressChangedEventHandler ProgressChanged;

        protected void OnProgressChanged(MtdbProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, e);
            }
        }

        #endregion

    }
}
