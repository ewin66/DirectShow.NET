using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;
using NUnit.Framework;

namespace DirectShowLib.Test
{
	[TestFixture]
	public class IAMAudioInputMixerTest
	{
		private IBaseFilter m_AudioFilter;
		private IAMAudioInputMixer m_iaim;

		public IAMAudioInputMixerTest()
		{
		}

		/// <summary>
		/// Test all IAMAudioInputMixer methods
		/// </summary>
		[Test]
		public void DoTests()
		{
            // Get an audio device
			m_AudioFilter = GetAudioFilter();

            // While some IAMAudioImputMixer methods work on both filters
            // and pins, some (like Enable) require a pin.  Get the first input pin.
            IPin inPin = DsFindPin.ByDirection(m_AudioFilter, PinDirection.Input, 0);

            // QI to an IAMAudioInputMixer
			m_iaim = inPin as IAMAudioInputMixer;

            try
            {
                TestMixLevel();
                TestEnable();
                TestMono();
                TestLoudness();
                TestBass();
                TestTreble();
                TestPan();
            }
            finally
            {
                if (m_AudioFilter != null)
                {
                    Marshal.ReleaseComObject(m_AudioFilter);
                    m_AudioFilter = null;
                }
                if (inPin != null)
                {
                    Marshal.ReleaseComObject(inPin);
                    inPin = null;
                }

                m_iaim = null;
            }
		}

        /// <summary>
        /// Test Get/Set Enable
        /// </summary>
        void TestEnable()
        {
            // MUST USE INPUT PIN, NOT FILTER

            int hr;
            bool pfEnable1, pfEnable2;

            try
            {
                // Read the current value
                hr = m_iaim.get_Enable(out pfEnable1);
                DsError.ThrowExceptionForHR(hr);

                // Put the reverse
                hr = m_iaim.put_Enable(!pfEnable1);
                DsError.ThrowExceptionForHR(hr);

                // Re-read the value
                hr = m_iaim.get_Enable(out pfEnable2);
                DsError.ThrowExceptionForHR(hr);

                // See if it changed
                Debug.Assert(pfEnable1 != pfEnable2, "Get/Set enable");

                // Put the original back
                hr = m_iaim.put_Enable(pfEnable1);
                DsError.ThrowExceptionForHR(hr);

                // Re-read the value
                hr = m_iaim.get_Enable(out pfEnable2);
                DsError.ThrowExceptionForHR(hr);

                // See if it changed
                Debug.Assert(pfEnable1 == pfEnable2, "Get/Set enable2");
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467263)
                {
                    System.Windows.Forms.MessageBox.Show("This audio card doesn't support Enable");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Test Get/Set Mono
        /// </summary>
        void TestMono()
        {
            int hr;
            bool pfMono1, pfMono2;

            try
            {
                // Read the current value
                hr = m_iaim.get_Mono(out pfMono1);
                DsError.ThrowExceptionForHR(hr);

                // Put the reverse
                hr = m_iaim.put_Mono(!pfMono1);
                DsError.ThrowExceptionForHR(hr);

                // Re-read the value
                hr = m_iaim.get_Mono(out pfMono2);
                DsError.ThrowExceptionForHR(hr);

                // See if it changed
                Debug.Assert(pfMono1 != pfMono2, "Get/Set Mono");

                // Put the original back
                hr = m_iaim.put_Mono(pfMono1);
                DsError.ThrowExceptionForHR(hr);

                // Re-read the value
                hr = m_iaim.get_Mono(out pfMono2);
                DsError.ThrowExceptionForHR(hr);

                // See if it changed
                Debug.Assert(pfMono1 == pfMono2, "Get/Set Mono2");
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467263)
                {
                    System.Windows.Forms.MessageBox.Show("This audio card doesn't support Mono");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Test Get/Set Loudness
        /// </summary>
        void TestLoudness()
        {
            int hr;
            bool pfLoudness1, pfLoudness2;

            try
            {
                // Read the current value
                hr = m_iaim.get_Loudness(out pfLoudness1);
                DsError.ThrowExceptionForHR(hr);

                // Put the reverse
                hr = m_iaim.put_Loudness(!pfLoudness1);
                DsError.ThrowExceptionForHR(hr);

                // Re-read the value
                hr = m_iaim.get_Loudness(out pfLoudness2);
                DsError.ThrowExceptionForHR(hr);

                // See if it changed
                Debug.Assert(pfLoudness1 != pfLoudness2, "Get/Set Loudness");

                // Put the original back
                hr = m_iaim.put_Loudness(pfLoudness1);
                DsError.ThrowExceptionForHR(hr);

                // Re-read the value
                hr = m_iaim.get_Loudness(out pfLoudness2);
                DsError.ThrowExceptionForHR(hr);

                // See if it changed
                Debug.Assert(pfLoudness1 == pfLoudness2, "Get/Set Loudness2");
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467263)
                {
                    System.Windows.Forms.MessageBox.Show("This audio card doesn't support Loudness");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Test Get/Set Bass
        /// </summary>
        void TestBass()
        {
            int hr;
            double BassValue1, BassValue2;
            double pRange;

            try
            {
                // Get max value
                hr = m_iaim.get_BassRange(out pRange);
                DsError.ThrowExceptionForHR(hr);

                // Read the current value
                hr = m_iaim.get_Bass(out BassValue1);
                DsError.ThrowExceptionForHR(hr);

                // write it
                hr = m_iaim.put_Bass(pRange);
                DsError.ThrowExceptionForHR(hr);

                // Read the value
                hr = m_iaim.get_Bass(out BassValue2);
                DsError.ThrowExceptionForHR(hr);

                // Make sure the value we set is what we just read
                Debug.Assert(pRange == BassValue2, "Put/Get Bass");

                // Put the original back
                hr = m_iaim.put_Bass(BassValue1);
                DsError.ThrowExceptionForHR(hr);
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467263)
                {
                    System.Windows.Forms.MessageBox.Show("This audio card doesn't support Bass");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Test Get/Set Treble
        /// </summary>
        void TestTreble()
        {
            int hr;
            double TrebleValue1, TrebleValue2;
            double pRange;

            try
            {
                // Get max value
                hr = m_iaim.get_TrebleRange(out pRange);
                DsError.ThrowExceptionForHR(hr);

                // Read the current value
                hr = m_iaim.get_Treble(out TrebleValue1);
                DsError.ThrowExceptionForHR(hr);

                // write it
                hr = m_iaim.put_Treble(pRange);
                DsError.ThrowExceptionForHR(hr);

                // Read the value
                hr = m_iaim.get_Treble(out TrebleValue2);
                DsError.ThrowExceptionForHR(hr);

                // Make sure the value we set is what we just read
                Debug.Assert(pRange == TrebleValue2, "Put/Get Treble");

                // Put the original back
                hr = m_iaim.put_Treble(TrebleValue1);
                DsError.ThrowExceptionForHR(hr);
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467263)
                {
                    System.Windows.Forms.MessageBox.Show("This audio card doesn't support Treble");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Test Get/Set pan
        /// </summary>
        void TestPan()
        {
            int hr;
            double PanValue1, PanValue2;
            const double pFixed = .77777;

            try
            {
                // Read the current value
                hr = m_iaim.get_Pan(out PanValue1);
                DsError.ThrowExceptionForHR(hr);

                // Write a different value
                hr = m_iaim.put_Pan(pFixed);
                DsError.ThrowExceptionForHR(hr);

                // Re-read the value
                hr = m_iaim.get_Pan(out PanValue2);
                DsError.ThrowExceptionForHR(hr);

                // See if the value changed.  Allow for imperfections from using double.
                Debug.Assert((int)(PanValue2 * 10000) == (int)(pFixed * 10000), "Get/Set Pan");

                // Put the original back
                hr = m_iaim.put_Pan(PanValue1);
                DsError.ThrowExceptionForHR(hr);
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467263)
                {
                    System.Windows.Forms.MessageBox.Show("This audio card doesn't support Pan");
                }
                else
                {
                    throw;
                }
            }
        }

        void TestMixLevel()
        {
            int hr;
            double MixLevelValue1, MixLevelValue2;
            const double pFixed = .77777;

            try
            {
                // Read the current value
                hr = m_iaim.get_MixLevel(out MixLevelValue1);
                DsError.ThrowExceptionForHR(hr);

                // Write a different value
                hr = m_iaim.put_MixLevel(pFixed);
                DsError.ThrowExceptionForHR(hr);

                // Re-read the value
                hr = m_iaim.get_MixLevel(out MixLevelValue2);
                DsError.ThrowExceptionForHR(hr);

                // See if the value changed.  Allow for imperfections from using double.
                Debug.Assert((int)(MixLevelValue2 * 10000) == (int)(pFixed * 10000), "Get/Set MixLevel");

                // Put the original back
                hr = m_iaim.put_MixLevel(MixLevelValue1);
                DsError.ThrowExceptionForHR(hr);
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467263)
                {
                    System.Windows.Forms.MessageBox.Show("This audio card doesn't support MixLevel");
                }
                else
                {
                    throw;
                }
            }
        }

        // Find an audio filter
        IBaseFilter GetAudioFilter()
        {
            DsDevice [] capDevices;

            // Get the collection of video devices
            capDevices = DsDevice.GetDevicesOfCat( FilterCategory.AudioInputDevice );
            if( capDevices.Length == 0 )
            {
                throw new Exception("No audio capture devices found!");
            }

            DsDevice dev = capDevices[0];
            string s;

            dev.Mon.GetDisplayName(null, null, out s);
            return Marshal.BindToMoniker( s ) as IBaseFilter;
        }
	}
}