﻿using FluentNHibernate.Mapping;

namespace MTDBFramework.Database
{
	/// <summary>
	/// NHibernate mapping for ConsensusProteinPair class
	/// </summary>
    public class ConsensusProteinPairMap : ClassMap<ConsensusProteinPair>
    {
		/// <summary>
		/// Constructor
		/// </summary>
        public ConsensusProteinPairMap()
        {
            Not.LazyLoad();
            Id(x => x.Id).Column("PairId").GeneratedBy.Native();
            Map(x => x.ConsensusId);
            Map(x => x.ProteinId);
            Map(x => x.CleavageState);
            Map(x => x.TerminusState);
            Map(x => x.ResidueStart);
            Map(x => x.ResidueEnd);
        }
    }
}
