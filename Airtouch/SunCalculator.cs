using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://www.pveducation.org/pvcdrom/properties-of-sunlight/declination-angle

namespace Airtouch {
    internal class SunCalculator {
      const double D2R = 0.01745329252;
      const double R2D = 57.29577951;
      const double EarthTilt = 23.45;
      const double siteLatitude = 34.0;

      private static double GetDecilation(DateTime now) =>
        -EarthTilt * Math.Cos(360.0 / 365.0 * (now.DayOfYear + 10.0));

      private static double GetSunElevation(DateTime now, double decilation, double HRA) {
        int dayOfYear = now.DayOfYear;
        double latitude = siteLatitude*D2R;
        double aux = Math.Sin(decilation) * Math.Sin(latitude) + Math.Cos(decilation) * Math.Cos(latitude) * Math.Cos(HRA);
        return Math.Asin(aux);
      }

      private static double GetSunAzimuth(double decilation, double HRA, double elevation) {
        double latitude = siteLatitude * D2R;
        double nom = Math.Sin(decilation) * Math.Cos(latitude) - Math.Cos(decilation) * Math.Sin(latitude) * Math.Cos(HRA);
        double denom = Math.Cos(elevation);
        double frac = nom / denom;
        if (frac >= 1.0 || frac <= -1.0)
          frac = 0.99999 * Math.Sign(frac);
        return Math.Acos(frac);
      }

      private static (double elev, double azim, double panel) GetSunAngles(DateTime now) {
        // calculate HRA
        double relDayTime = (now - DateTime.Now.Date.AddHours(12)).TotalHours;
        double HRA = relDayTime * 15.0 * D2R;
        // calculate decilation
        double decilation = GetDecilation(now);
        // calculate elevation
        double elevation = GetSunElevation(now, decilation, HRA);
        // calculate azimuth
        double azimuth = GetSunAzimuth(decilation, HRA, elevation);
        // calculate panel angle
        double panel = Math.Atan2(Math.Cos(elevation) * Math.Sin(azimuth), Math.Sin(elevation));
        // calculate azimuth
        return (elevation * R2D, azimuth * R2D, panel * R2D);
      }


      public static void Run() {
        DateTime start = DateTime.Now.Date.AddHours(9);
        DateTime stop = start.AddHours(6);
        while (start <= stop) {
          (double elev, double azim, double panel) = GetSunAngles(start);
          Console.WriteLine($"{start}, {elev}, {azim} => {panel}");
          start = start.AddMinutes(15.0);
        }
      }
    }
}
