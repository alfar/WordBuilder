using System;
using System.Collections.Generic;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.Helpers;

namespace Whee.WordBuilder.Exporters
{
	public interface IExporter
	{
		string Name { get; }
		void Export(List<Context> list, string path, IFileSystem fileSystem);
	}
}
