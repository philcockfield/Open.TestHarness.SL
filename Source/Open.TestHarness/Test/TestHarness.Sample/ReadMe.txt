------------------------------------------------------------------------------
                        TestHarness Sample Solution
------------------------------------------------------------------------------

1. Links
2. Pre-reqs
3. Lib DLL blocking (possibly with Windows download)
4. Open-source licence

------------------------------------------------------------------------------

1. Links

Blog: http://TestHarness.org
Source repository: http://github.com/philcockfield/Open.TestHarness


------------------------------------------------------------------------------

2. Pre-reqs
    - Visual Studio 2010
    - Silverlight Version 4

------------------------------------------------------------------------------

3. Lib DLL blocking (possibly with Windows download)

On some versions of Windows (or some browsers), when downloading the ZIP file
the DLL's in the Lib folder are blocked.  This will cause the solution to fail at run-time.
To unblock the files :

    - Go to the /Lib/Open.Core/ folder
    - Bring up the Properties window on each DLL file.
    - Click the "Unblock" button.	   

------------------------------------------------------------------------------

4. Open-source licence

The TestHarness and Open.Core are open-source, under the super liberal MIT licence, 
which basically says "do what you want".  Here's a copy of that license:

//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------












