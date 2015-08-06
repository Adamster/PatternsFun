using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Paddock;

namespace Domain.Patterns.Visitor
{
    public class ArtefactCollectorVisitor : IVisitor
    {
        public List<String> ArtefactList { get; set; }

       public ArtefactCollectorVisitor()
        {
            ArtefactList = new List<string>();
        }
        #region Implementation of IVisitor

        public void Visit(Boxes box)
        {
            ArtefactList.Add("Autographs from "+ box.Owner + " team");
        }

        public void Visit(PitLane lane)
        {
            ArtefactList.Add("Photos from "+ lane.Name);
        }

        #endregion
    }
}
