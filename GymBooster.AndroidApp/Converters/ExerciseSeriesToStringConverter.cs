using System;
using System.Globalization;
using System.Linq;
using GymBooster.Common.Objects.DTO;
using Xamarin.Forms;

namespace GymBooster.AndroidApp.Converters
{
    public class ExerciseSeriesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var exercise = value as ExerciseDTO;
            if (exercise == null)
                return null;

            var seriesMergedInfo = string.Join(",",
                exercise.Series.Select(s =>
                    $"{s.NumberOfReps}x{s.Weight}kg(s) {(string.IsNullOrEmpty(s.Comment) ? string.Empty : "(")}{s.Comment}{(string.IsNullOrEmpty(s.Comment) ? string.Empty : ")")}"));

            return seriesMergedInfo;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
