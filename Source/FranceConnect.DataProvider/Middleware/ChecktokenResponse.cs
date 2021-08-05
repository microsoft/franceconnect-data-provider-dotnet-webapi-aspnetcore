//
// The MIT License (MIT)
// Copyright (c) 2016 Microsoft France
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// You may obtain a copy of the License at https://opensource.org/licenses/MIT
//

using System;

namespace FranceConnect.DataProvider.Middleware
{
    public class ChecktokenResponse
    {
        public string[] Scope { get; set; }
        public Identity Identity { get; set; }
        public Client Client { get; set; }
        public string Identity_provider_id { get; set; }
        public string Identity_provider_host { get; set; }
        public string acr { get; set; }
    }

    public class Identity
    {
        public string Given_name { get; set; }
        public string Family_name { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Gender { get; set; }
        public string Birthplace { get; set; }
        public string Birthcountry { get; set; }
        public string Email { get; set; }
    }

    public class Client
    {
        public string Client_id { get; set; }
        public string Client_name { get; set; }
    }
}
