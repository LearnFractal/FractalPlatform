using FractalPlatform.Client;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls.Grid;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FractalPlatform.Examples.Applications.Forum
{
    public class RenderForm: BaseRenderForm
    {
        public RenderForm(BaseApplication application, DOMForm form) : base(application, form)
        {
        }

        public override string Layout => null; //do not use form layout

        public class GridInfo
        {
            public string Title { get; set; }
            
            public string Who { get; set; }

            public uint CountMessages { get; set; }

            public uint CountViews { get; set; }
        }

        string html = @"
            <div class='leftDiv'>@Who</div>            
            <div class='leftDiv' style='width:10px; height: 10px;'></div>
            <div class='leftDiv'>@OnDate</div>
            <br/>
            <div class='title'>@Title</div>
            <div class='picture'>@Picture</div>
            <br/>
            <div class='block'>
            <div class='text' tabindex='0'>@Text</div>
            <br/>
            <div  id='parent'>
                <table>
                    <tr style='height:50px'>
                        <td width='100%' nowrap>
                            <div class='newbutton black' onclick=""@ReadMore"">Read more</div>
                        </td>
                        <td nowrap valign='middle'>
                            <div  id='Comments'; >@Comments</div>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td nowrap valign='middle'>
                            <div id='like'>@Likes</div>
                        </td>
                    </tr>

                </table>
             </div>
            </div>
            <hr/>";

        public override string RenderGrid(GridDOMControl domControl)
        {
            if(domControl.Key == "Messages")
            {
                var sb = new StringBuilder();
                                
                foreach(DataRow dr in domControl.DataTable.Rows)
                {
                    sb.Append(@$"<tr>
                                    <td style='border:1px solid white' align='center'>
                                       <img style='max-width:120px;max-height:120px' src='{GetFileUrl(dr["Avatar"].ToString())}'>
                                       <div>{dr["Who"]}</div>
                                       <div>{dr["OnDate"]}</div>
                                    </td>
                                    <td  style='border:1px solid white' align='left' valign='top'>
                                        <div>{dr["Message"]}</div>
                                    </td>
                                 </tr>");
                }

                return sb.ToString();
            }
            else
            {
                return base.RenderGrid(domControl);
            }
        }
    }
}
