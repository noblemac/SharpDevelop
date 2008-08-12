﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Windows;
using System.Windows.Media;

namespace ICSharpCode.WpfDesign.Designer
{
	public static class ExtensionMethods
	{
		public static double Coerce(this double d, double min, double max)
		{
			return Math.Max(Math.Min(d, max), min);
		}

		public static void AddRange<T>(this ICollection<T> col, IEnumerable<T> items)
		{
			foreach (var item in items) {
				col.Add(item);
			}
		}

		public static object GetDataContext(this RoutedEventArgs e)
		{
			var f = e.OriginalSource as FrameworkElement;
			if (f != null) {
				return f.DataContext;
			}
			return null;
		}

		public static T FindAncestor<T>(this DependencyObject d, string name) where T : class
        {
            while (true) {
                if (d == null) return null;
                if (d is T && d is FrameworkElement && (d as FrameworkElement).Name == name) return d as T;
                d = VisualTreeHelper.GetParent(d);
            }
        }
	}
}
