﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace fx
{
    class Tools
    {
        public static void checkInput(TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
        public static bool isInputNumber(KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
               e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.OemPeriod)
            {
                //按下了Alt、ctrl、shift等修饰键  
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
                else
                {
                    return true;
                }

            }
            else//按下了字符等其它功能键  
            {
                e.Handled = true;
            }
            return false;
        }
        public static bool isInputNumber1to6(KeyEventArgs e)
        {
            if ((e.Key >= Key.D1 && e.Key <= Key.D6) || (e.Key >= Key.NumPad1 && e.Key <= Key.NumPad6) ||
               e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right)
            {
                //按下了Alt、ctrl、shift等修饰键  
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
                else
                {
                    return true;
                }

            }
            else//按下了字符等其它功能键  
            {
                e.Handled = true;
            }
            return false;
        }
    }
}
