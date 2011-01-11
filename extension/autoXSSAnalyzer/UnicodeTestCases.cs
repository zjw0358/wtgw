using System;
using System.Collections.Generic;
using System.Text;
using Casaba;

namespace Casaba
{
    public class UnicodeTestCases : List<UnicodeTestCase>
    {
        public UnicodeTestCase GetMappingFromSourceCodePoint(uint sCodePoint){
            
            foreach(UnicodeTestCase item in this){
                if (item.SourcePoint.CodePoint == sCodePoint)
                {
                    return item;
                }
            }
            return null;
        }

        public UnicodeTestCases GetMappingsByType(UnicodeTestCaseTypes type){
            UnicodeTestCases ret = new UnicodeTestCases();
            foreach(UnicodeTestCase mapping in this){
                if (mapping.Type == type)
                {
                    ret.Add(mapping);
                }
            }
            return ret;
        }

        public UnicodeTestCases GetAll()
        {
            UnicodeTestCases ret = new UnicodeTestCases();
            foreach (UnicodeTestCase mapping in this)
            {
                ret.Add(mapping);
            }
            return ret;
        }
    }
}
