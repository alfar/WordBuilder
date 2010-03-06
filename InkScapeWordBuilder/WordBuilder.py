#!/usr/bin/env python 
'''
Copyright (C) 2006 Jos Hirth, kaioa.com

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
'''
'''
Example filltext sentences generated over at http://lipsum.com/
'''

import os
import sys
import inkex
import subprocess

args = ['e:\\code\\lingo\\InkScapeWordBuilder\\bin\\release\\InkScapeWordBuilder.exe']

for arg in sys.argv[1:]:
    args.append(arg)

st = subprocess.Popen(args, stdout=subprocess.PIPE, stderr=subprocess.PIPE)

(stdout, stderr) = st.communicate()

sys.stdout.write(stdout)
sys.stderr.write(stderr)
#sys.stderr.write(stdout)

#inkex.debug(sys.argv[1:]);
#os.execv('e:\\code\\lingo\\InkScapeWordBuilder\\bin\\release\\InkScapeWordBuilder.exe', sys.argv[1:])


