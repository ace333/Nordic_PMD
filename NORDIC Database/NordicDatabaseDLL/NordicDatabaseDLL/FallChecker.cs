using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordicDatabaseDLL
{
    public class FallChecker
    {
        private const int border = 5;
        private bool isFucked;


        public bool CheckIfFall(float[] dataX, float[] dataY, float[] dataZ, float delta, int samplesAmount)
        {
            float[] validX, validY, validZ;

            if(!CheckIfTabIsLongEnough(dataX) || !CheckIfTabIsLongEnough(dataX) || !CheckIfTabIsLongEnough(dataZ))
            {
                return false;
            }

            //operations on last 100 samples
            validX = ObtainLastValidValuesFromTab(dataX);
            validY = ObtainLastValidValuesFromTab(dataY);
            validZ = ObtainLastValidValuesFromTab(dataZ);

            //next calculations done only when isFucked is false
            CheckSamples(dataX, delta, samplesAmount);
            if (!isFucked)
                CheckSamples(dataY, delta, samplesAmount);
            if (!isFucked)
                CheckSamples(dataZ, delta, samplesAmount);

            return isFucked;
        }

        private void CheckSamples(float[] data, float delta, int samplesAmount)
        {
            int maxIndex = border - samplesAmount;
            float[] calcTab = new float[samplesAmount];

            for(int i=0; i <= maxIndex; i++)
            {
                //filling calcTab with data from data Table
                for(int j=i; j<samplesAmount; j++)
                {
                    calcTab[j] = data[j];
                }

                DoDeltaCalculations(calcTab, delta);
            }

        }

        private void DoDeltaCalculations(float[] dataToAnalyze, float delta)
        {
            for(int i=0; i<dataToAnalyze.Length; i++)
            {
                float sample = dataToAnalyze[i];

                for(int j=0; j<dataToAnalyze.Length; j++)
                {
                    if(i!=j)
                    {
                        float absDelta = Math.Abs(sample) - Math.Abs(dataToAnalyze[j]);

                        if (absDelta > delta)
                        {
                            isFucked = true;
                            return;
                        }
                            
                    }
                }
            }
        }

        private bool CheckIfTabIsLongEnough(float[] tab)
        {
            if (tab.Length < border)
                return false;
            else
                return true;
        }

        private float[] ObtainLastValidValuesFromTab(float[] tab)
        {
            float[] validTab = new float[border];
            int index = tab.Length - border;
            int iter = 0;

            for(int i=index; i<tab.Length; i++)
            {
                validTab[iter++] = tab[i];
            }

            return validTab;
        }       

    }
}
