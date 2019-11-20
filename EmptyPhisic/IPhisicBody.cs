using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoEmpty.Component;

namespace MonoEmpty.Component.Phisic;
{
    interface IPhisicBody:IUpdateComponent
    {
        Transform transform { get; set; }
        World world { get; set; }
        float Mass { get; set; }
    }
}
