﻿using System.Collections.Generic;
using MTDBFramework.Algorithms.Clustering;
using MTDBFramework.Data;
using MTDBFramework.UI;

namespace MTDBFramework.Database
{
    public class SqLiteTargetDatabaseWriter : ITargetDatabaseWriter
    {

        // TODO: Implement these (or maybe dictionaries)
        private readonly Dictionary<string, TargetPeptideInfo> m_uniquePeptides = new Dictionary<string, TargetPeptideInfo>();
        private readonly Dictionary<string, TargetDataSet> m_uniqueDataSets = new Dictionary<string, TargetDataSet>();

        public void Write(TargetDatabase database, Options options, string path)
        {

            DatabaseCreatorFactory.DbFile = path;
            var sessionFactory = DatabaseCreatorFactory.CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                // populate the database
                using (var transaction = session.BeginTransaction())
                {

                    /* This section breaks up the Target object, pulling out the individual TargetDataSet,  SequenceInfo,
                     * and TargetPeptideInfo. These objects are then "reverse linked", so that each of these objects 
                     * relates to multiple evidences. This is because these objects need to know what they are related to.
                     * Additionally, these objects are saved before the Evidences are, because these objects need to already
                     * exist in order to properly generate the relation. 
                     * */
                    int current = 0;
                    int total = database.ConsensusTargets.Count;
                    session.Save(options);
                    foreach (ConsensusTarget consensusTarget in database.ConsensusTargets)
                    {
                        consensusTarget.Id = ++current;
                        foreach (Evidence t in consensusTarget.Evidences)
                        {
                            if (!m_uniquePeptides.ContainsKey(t.PeptideInfo.Peptide))
                            {
                                m_uniquePeptides.Add(t.PeptideInfo.Peptide, t.PeptideInfo);
                            }
                            t.PeptideInfo = m_uniquePeptides[t.PeptideInfo.Peptide];
                            if (!m_uniqueDataSets.ContainsKey(t.DataSet.Path))
                            {
                                m_uniqueDataSets.Add(t.DataSet.Path, t.DataSet);
                            }
                            t.DataSet = m_uniqueDataSets[t.DataSet.Path];
                            t.Parent = consensusTarget;
                            
                        }
                        consensusTarget.Dataset = consensusTarget.Evidences[0].DataSet;
                        
                        session.SaveOrUpdate(consensusTarget);
                    }
                    current = -1;
                    total = 0;

                    OnProgressChanged(new MtdbProgressChangedEventArgs(current, total, MtdbCreationProgressType.COMMIT.ToString()));

                    transaction.Commit();
                    session.Close();
                }
            }
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