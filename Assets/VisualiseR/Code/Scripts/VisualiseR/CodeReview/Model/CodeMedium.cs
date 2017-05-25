using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualiseR.CodeReview
{
    [Serializable]
    public class CodeMedium : ICodeMedium
    {
        public string Name { get; set; }

        public List<ICode> CodeFragments { get; set; }

        public CodeMedium()
        {
            CodeFragments = new List<ICode>();
        }

        public void AddCodeFragment(ICode picture)
        {
            CodeFragments.Add(picture);
        }

        public void RemoveCodeFragment(ICode picture)
        {
            CodeFragments.Remove(picture);
        }

        public ICode GetCodeFragment(int pos)
        {
            return CodeFragments.ElementAt(pos);
        }

        public int GetCodeFragmentPos(ICode code)
        {
            return CodeFragments.IndexOf(code);
        }

        public List<ICode> GetCodeFragmentsWithRate(Rate rate)
        {
            List<ICode> codeFragmentsWithRate = new List<ICode>();
            foreach (var code in CodeFragments)
            {
                if (code.Rate.Equals(rate))
                {
                    codeFragmentsWithRate.Add(code);
                }
            }
            return codeFragmentsWithRate;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CodeMedium[Name: {0}, CodeFragments: [", Name);
            foreach (ICode pic in CodeFragments)
            {
                sb.Append(pic + ", ");
            }
            sb.Append("]");
            return sb.ToString();
        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || CodeFragments.Count == 0;
        }
    }
}