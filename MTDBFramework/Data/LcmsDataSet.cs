﻿#region Namespaces

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MTDBFramework.UI;
using Regressor.Algorithms;

#endregion

namespace MTDBFramework.Data
{
    public class LcmsDataSet : ObservableObject
    {
        #region Private Fields

        private string m_name;
        private LcmsIdentificationTool m_tool;
        private LinearRegressionResult m_regressionResult;

        #endregion

        #region Public Properties

        public string Name
        {
            get { return m_name; }
            set
            {
                m_name = value;
                OnPropertyChanged("Name");
            }
        }

        public LcmsIdentificationTool Tool
        {
            get { return m_tool; }
            set
            {
                m_tool = value;
                OnPropertyChanged("Tool");
            }
        }

        public ObservableCollection<Evidence> Evidences { get; set; }

        public LinearRegressionResult RegressionResult
        {
            get { return m_regressionResult; }
            set
            {
                m_regressionResult = value;
                OnPropertyChanged("RegressionResult");
            }
        }

        #endregion 

        public LcmsDataSet()
        {
            Name = String.Empty;
            Tool = LcmsIdentificationTool.Raw;
            Evidences = new ObservableCollection<Evidence>();
            RegressionResult = new LinearRegressionResult();
        }

        public LcmsDataSet(string name, LcmsIdentificationTool tool, IEnumerable<Evidence> evidences)
        {
            Name = name;
            Tool = tool;
            Evidences = new ObservableCollection<Evidence>(evidences);
            RegressionResult = new LinearRegressionResult();
        }
    }
}