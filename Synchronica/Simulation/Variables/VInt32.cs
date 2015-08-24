using Synchronica.Simulation.Curves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synchronica.Simulation.Variables
{
    public sealed class VInt32
    {
        int m_value;
        int m_milliseconds;

        public void Up(int millisecond)
        {
        }

        public void Down(int millisecond)
        {
        }

        public void TrimBefore(int millisecond)
        {
        }

        public void TrimAfter(int millisecond)
        {
        }

        public void AddCurves(IEnumerable<Curve> curves)
        {
        }
    }
}