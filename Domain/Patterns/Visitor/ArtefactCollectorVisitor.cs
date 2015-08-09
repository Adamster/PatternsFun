// File: ArtefactCollectorVisitor.cs in
// PatternsFun by Serghei Adam 
// Created 06 08 2015 
// Edited 07 08 2015

using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Paddock;

namespace Domain.Patterns.Visitor
{
    public class ArtefactCollectorVisitor : IVisitor
    {
        public ArtefactCollectorVisitor()
        {
            ArtefactList = new List<string>();
        }

        public List<string> ArtefactList { get; set; }

        #region Implementation of IVisitor

        public void Visit(Boxes box)
        {
            ArtefactList.Add("Autographs from " + box.Owner + " team");
        }

        public void Visit(PitLane lane)
        {
            ArtefactList.Add("Photos from " + lane.Name);
        }

        #endregion
    }
}