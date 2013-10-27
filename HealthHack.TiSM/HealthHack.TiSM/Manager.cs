﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthHack.TiSM.Model;
using HealthHack.TiSM.DAL;

namespace HealthHack.TiSM
{
    public class Manager
    {
        public List<Mutation> GetMutationList(string fileContentString)
        {
            List<Mutation> list = new List<Mutation>();
            try
            {
                string[] fileLines = fileContentString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                for (int i = 1; i < fileLines.Length; i++)
                {
                    string fileLine = fileLines[i];
                    string[] items = fileLine.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    Mutation m = new Mutation();
                    m.Gene = items[0];
                    m.Transscript = items[1];
                    m.AAMutation = items[2];
                    m.CDSMutation = new MutationPoint(items[3]);
                    m.Somaticstatus = items[4];
                    m.Zygosity = items[5];
                    m.Validated = items[6];
                    m.Type = items[7];
                    m.Position = items[8];
                    list.Add(m);
                }
                list = list.Where(x=> x.Type.ToUpper() =="SUBSTITUTION" ).ToList();
                list = list.Where(x => x.Transscript.Substring(0,4).ToUpper() == "ENST").ToList();
            }
            catch { }
            return list;
        }

        public Mutation GetMutationPattern(Mutation mutation, string geneSequence)
        {
            switch (mutation.Type.ToUpper())
            {
                case "SUBSTITUTION":
                    {
                        mutation.MC1Position  = mutation.CDSMutation.Position % 3;
                        if (mutation.MC1Position == 0) mutation.MC1Position = 3;
                        string window = GetWindow(geneSequence, mutation.CDSMutation.Position - 1, mutation.MC1Position);
                        mutation.PreviousCodon = window.Substring(0, 3);
                        mutation.MutatedCodon = window.Substring(3, 3);
                        mutation.NextCodon = window.Substring(6, 3);
                        break;
                    }

            }
            return mutation;
        }

        public bool PersistToDB(List<Mutation> list)
        {
            MutationManager m = new MutationManager();
            return m.PersistMutationList(list);
        }

        #region private functions
        private string GetWindow(string geneSequence, int sequencePosition, int codonPosition)
        {
            string returnString = string.Empty;
            switch (codonPosition)
            {
                case 1:
                    {
                        returnString = geneSequence.Substring(sequencePosition-3, 9);
                        break;
                    }
                case 2:
                    {
                        returnString = geneSequence.Substring(sequencePosition-4, 9);
                        break;
                    }
                default:
                    {
                        returnString = geneSequence.Substring(sequencePosition -5, 9);
                        break;
                    }
            }
            return returnString;
        }
        #endregion

    }
}
