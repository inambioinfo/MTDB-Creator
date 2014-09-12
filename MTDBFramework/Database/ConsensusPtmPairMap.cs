﻿using FluentNHibernate.Mapping;
using MTDBFramework.Data;

namespace MTDBFramework.Database
{
    /// <summary>
    /// Fluent NHibernate mapping for ConsensusPtmPair class
    /// </summary>
    public class ConsensusPtmPairMap : ClassMap<ConsensusPtmPair>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConsensusPtmPairMap()
        {
            Not.LazyLoad();
            Id(x => x.Id).Column("PairId").GeneratedBy.Native();
            Map(x => x.ConsensusId).Column("TargetId");
            Map(x => x.PtmId).Column("PostTranslationModId");
            Map(x => x.Location);
        }
    }
}
