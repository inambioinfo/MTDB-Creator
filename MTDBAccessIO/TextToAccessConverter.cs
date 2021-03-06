﻿using System.IO;
using MTDBFramework.IO;
using ACCESS = Microsoft.Office.Interop.Access;

namespace MTDBAccessIO
{
    public class TextToAccessConverter : ITextToDbConverter
    {
        public void ConvertToDbFormat(string path)
        {

            var accApplication = new ACCESS.Application();

            var pieces = path.Split('\\');
            string directory = "";
            foreach (var piece in pieces)
            {
                if (piece.Contains("."))
                {
                    continue;
                }
                directory += piece;
                directory += "\\";
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            accApplication.NewCurrentDatabase(path);
            accApplication.DoCmd.TransferText(TransferType: ACCESS.AcTextTransferType.acImportDelim,
                TableName: "T_Mass_Tags", FileName: directory + "tempMassTags.txt", HasFieldNames: true);
            accApplication.DoCmd.TransferText(TransferType: ACCESS.AcTextTransferType.acImportDelim,
                TableName: "T_Mass_Tags_NET", FileName: directory + "tempMassTagsNet.txt", HasFieldNames: true);
            accApplication.DoCmd.TransferText(TransferType: ACCESS.AcTextTransferType.acImportDelim,
                TableName: "T_Proteins", FileName: directory + "tempProteins.txt", HasFieldNames: true);
            accApplication.DoCmd.TransferText(TransferType: ACCESS.AcTextTransferType.acImportDelim,
                TableName: "T_Mass_Tags_to_Protein_Map", FileName: directory + "tempMassTagToProteins.txt", HasFieldNames: true);
            accApplication.DoCmd.TransferText(TransferType: ACCESS.AcTextTransferType.acImportDelim,
                TableName: "T_Analysis_Description", FileName: directory + "tempAnalysisDescription.txt", HasFieldNames: true);
            accApplication.DoCmd.TransferText(TransferType: ACCESS.AcTextTransferType.acImportDelim,
                TableName: "V_Filter_Set_Overview_Ex", FileName: directory + "tempFilterSet.txt", HasFieldNames: true);
            accApplication.CloseCurrentDatabase();
            accApplication.Quit();

            File.Delete(directory + "tempMassTags.txt");
            File.Delete(directory + "tempPeptides.txt");
            File.Delete(directory + "tempModInfo.txt");
            File.Delete(directory + "tempMassTagsNet.txt");
            File.Delete(directory + "tempProteins.txt");
            File.Delete(directory + "tempMassTagToProteins.txt");
            File.Delete(directory + "tempAnalysisDescription.txt");
            File.Delete(directory + "tempFilterSet.txt");
        }
    }
}
