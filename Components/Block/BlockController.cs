/*
' Copyright (c) 2014 Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/
using System.Collections.Generic;
using DotNetNuke.Data;
using System.Linq;

namespace Satrabel.Modules.OpenBlocks.Components
{
    public class BlockController
    {
        public void CreateBlock(Block t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Block>();
                rep.Insert(t);
            }
        }

        public void DeleteBlock(int blockId, int portalId)
        {
            var t = GetBlock(blockId, portalId);
            DeleteBlock(t);
        }

        public void DeleteBlock(Block t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Block>();
                rep.Delete(t);
            }
        }

        public IEnumerable<Block> GetBlocks(int portalId)
        {
            IEnumerable<Block> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Block>();
                t = rep.Get(portalId);
            }
            return t;
        }

        public Block GetBlock(int blockId, int portalId)
        {
            Block t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Block>();
                t = rep.GetById(blockId, portalId);
            }
            return t;
        }

        public void UpdateBlock(Block t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Block>();
                rep.Update(t);
            }
        }


        public Block GetBlock(string name, int portalId, string Culturecode)
        {            
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Block>();
                var blockLst = rep.Find("WHERE Name = @0 and PortalId = @1", name, portalId);
                blockLst = blockLst.Where(b => b.CultureCode == Culturecode || string.IsNullOrEmpty(b.CultureCode)).OrderByDescending(b => b.CultureCode);
                foreach (var b in blockLst)
                {
                    return b;
                }
            }
            return null;
        }
    }
}
