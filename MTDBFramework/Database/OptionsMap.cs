﻿using FluentNHibernate.Mapping;
using MTDBFramework.Data;


namespace MTDBFramework.Database
{
    class OptionsMap : ClassMap<Options>
    {
        public OptionsMap()
        {
            Not.LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity();

            // Regression
            Map(x => x.RegressionType);
            Map(x => x.RegressionOrder);

            //// General
            Map(x => x.TargetFilterType);

            ////public static IRetentionTimePredictor Predictor { get; set; }
            Map(x => x.PredictorType);

            //// Peptides
            Map(x => x.MaxModsForAlignment);
            Map(x => x.MinObservationsForExport);

            Map(x => x.ExportTryptic);
            Map(x => x.ExportPartiallyTryptic);
            Map(x => x.ExportNonTryptic);

            //// TODO: These three are arrays of length 3. Try to do error detection

            Map(x => x.MinXCorrForExportTrytpic);
            Map(x => x.MinXCorrForExportPartiallyTrytpic);
            Map(x => x.MinXCorrForExportNonTrytpic);

            //// Sequest
            Map(x => x.MinXCorrForAlignment);
            Map(x => x.UseDelCn);
            Map(x => x.MaxDelCn);

            //// Xtandem
            Map(x => x.MaxLogEValForXTandemAlignment);
            Map(x => x.MaxLogEValForXTandemExport);

            //// Other
            Map(x => x.MaxRankForExport);
        }
    }
}