using System;
using System.Collections.Generic;

using UnityEngine;

namespace ASE
{
    public interface IAtmoDataProvider
    {
        float GetMach();
        double GetDensity();
    }

    public class FallbackAtmoDataProvider : IAtmoDataProvider
    {
        public float GetMach()
        {
            float temperature = FlightGlobals.ActiveVessel.flightIntegrator.getExternalTemperature();
            float sos = Mathf.Sqrt(1.4f * (temperature + 273.15f) * 287f);//Ideal gas
            float speed = FlightGlobals.ActiveVessel.GetSrfVelocity().magnitude;
            return speed / sos;
        }

        public double GetDensity()
        {
            return FlightGlobals.ActiveVessel.atmDensity;
        }
    }

    public class AtmoDataProvider
    {
        private static IAtmoDataProvider _adp;

        public static IAtmoDataProvider Get()
        {
            if (_adp != null)
				return _adp;
            _adp = new FallbackAtmoDataProvider();
            return _adp;
        }
    }
}
