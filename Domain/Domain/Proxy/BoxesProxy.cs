using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Interfaces;
using Domain.Domain.Paddock;
using Domain.Domain.Persons;

namespace Domain.Domain.Proxy
{
    public class BoxesProxy : IAccess
    {
        private readonly Boxes _realBoxes;
        private readonly List<Fan> _realFans = new List<Fan>();

        public BoxesProxy(List<Fan> fan, Boxes boxes)
        {
            _realFans = fan;
            _realBoxes = boxes;
        }

        public BoxesProxy(Fan fan, Boxes boxes)
        {
            _realBoxes = boxes;
            _realFans.Add(fan);
        }

        #region Implementation of IAccess

        public void GrantAcces()
        {
            foreach (var realFan in _realFans)
            {
                if (realFan.AccessLevel == PaddockAccessLevels.ClubPaddock)
                {
                    _realBoxes.GrantAcces();
                }
                else
                {
                    Console.WriteLine("Sorry you don't have acces to {0} boxes", _realBoxes.Owner);
                }
            }

        }

        #endregion
    }
}
