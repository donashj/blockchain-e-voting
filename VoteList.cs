using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project
{
    public class VoteList
    {
        public string id { set; get; }
        public string voterid { set; get; }
        public string cid { set; get; }
        public string eid { set; get; }
        public string election { set; get; }
        public string vname { set; get; }
        public string datetime { set; get; }                
        public string block { set; get; }
        public string previoushash { set; get; }
    }
}