﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.Core;

namespace ICSharpCode.SharpDevelop
{
	/// <summary>
	/// A wrapper around commonly used System.IO methods
	/// for accessing the file system. Allows mocking file system access in unit tests.
	/// </summary>
	[SDService("SD.FileSystem", FallbackImplementation = typeof(EmptyFileSystem))]
	public interface IFileSystem : IReadOnlyFileSystem
	{
		/// <inheritdoc cref="System.IO.File.Delete"/>
		void Delete(FileName path);
		
		/// <inheritdoc cref="System.IO.File.CopyFile"/>
		void CopyFile(FileName source, FileName destination, bool overwrite = false);
		
		/// <inheritdoc cref="System.IO.Directory.Delete"/>
		void CreateDirectory(DirectoryName path);
		
		/// <inheritdoc cref="System.IO.File.OpenWrite"/>
		Stream OpenWrite(FileName fileName);
	}
	
	public interface IReadOnlyFileSystem
	{
		/// <inheritdoc cref="System.IO.File.Exists"/>
		bool FileExists(FileName path);
		
		/// <inheritdoc cref="System.IO.Directory.Exists"/>
		bool DirectoryExists(DirectoryName path);
		
		/// <inheritdoc cref="System.IO.File.OpenRead"/>
		Stream OpenRead(FileName fileName);
		
		/// <inheritdoc cref="System.IO.File.OpenText"/>
		TextReader OpenText(FileName fileName);
		
		IEnumerable<FileName> GetFiles(DirectoryName directory, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
	}
	
	sealed class EmptyFileSystem : IFileSystem
	{
		void IFileSystem.Delete(FileName path)
		{
			throw new FileNotFoundException();
		}
		
		void IFileSystem.CopyFile(FileName source, FileName destination, bool overwrite)
		{
			throw new FileNotFoundException();
		}
		
		void IFileSystem.CreateDirectory(DirectoryName path)
		{
			throw new NotSupportedException();
		}
		
		Stream IFileSystem.OpenWrite(FileName fileName)
		{
			throw new NotSupportedException();
		}
		
		bool IReadOnlyFileSystem.FileExists(FileName path)
		{
			return false;
		}
		
		bool IReadOnlyFileSystem.DirectoryExists(DirectoryName path)
		{
			return false;
		}
		
		Stream IReadOnlyFileSystem.OpenRead(FileName fileName)
		{
			throw new FileNotFoundException();
		}
		
		TextReader IReadOnlyFileSystem.OpenText(FileName fileName)
		{
			throw new FileNotFoundException();
		}
		
		IEnumerable<FileName> IReadOnlyFileSystem.GetFiles(DirectoryName directory, string searchPattern, SearchOption searchOption)
		{
			return Enumerable.Empty<FileName>();
		}
	}
}
