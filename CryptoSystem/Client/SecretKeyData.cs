using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XTR_TwofishAlgs.MathBase.GaloisField;

namespace Client
{
    public sealed class SecretKeyData
    {
        private GFP2.Polynom1DegreeCoeffs? traceG;

        public GFP2.Polynom1DegreeCoeffs? TraceG
        {
            get { return traceG; }
            set { traceG = value; }
        }

        private BigInteger? p;

        public BigInteger? P
        {
            get { return p; }
            set { p = value; }
        }

        private BigInteger? q;

        public BigInteger? Q
        {
            get { return q; }
            set { q = value; }
        }

        private BigInteger secretA;

        public BigInteger SecretA
        {
            get { return secretA; }
            set { secretA = value; }
        }

    }
}
