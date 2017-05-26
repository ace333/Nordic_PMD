/*
 * Copyright (c) 2015, Arkadiusz Chudy, Bartłomiej Cerek.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

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
