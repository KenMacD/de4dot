﻿/*
    Copyright (C) 2011-2012 de4dot@gmail.com

    This file is part of de4dot.

    de4dot is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    de4dot is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with de4dot.  If not, see <http://www.gnu.org/licenses/>.
*/

using Mono.Cecil;
using Mono.Cecil.Metadata;
using de4dot.blocks;

namespace de4dot.code.deobfuscators.Eazfuscator_NET {
	class EfConstantsReader : ConstantsReader {
		public EfConstantsReader(MethodDefinition method)
			: base(method) {
			initialize();
		}

		void initialize() {
			findConstants();
		}

		void findConstants() {
			for (int index = 0; index < instructions.Count; ) {
				int value;
				if (!getInt32(ref index, out value))
					break;
				var stloc = instructions[index];
				if (!DotNetUtils.isStloc(stloc))
					break;
				var local = DotNetUtils.getLocalVar(locals, stloc);
				if (local == null || local.VariableType.EType != ElementType.I4)
					break;
				localsValues[local] = value;
				index++;
			}

			if (localsValues.Count != 2)
				localsValues.Clear();
		}
	}
}
