using FractalPlatform.Client;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Client.UI.DOM.Controls.Component;
using FractalPlatform.Client.UI.DOM.Controls.Grid;
using System;
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

        public override string RenderStyles(DOMForm form)
        {
            return @"
/* Hide some info on small screens that are 992px or less. */
@media screen and (max-width: 992px) {
  .hide1 {
    display:none;
  }
}
/* Hide even more on smaller screens that are 768px or less. */
@media screen and (max-width: 768px) {
  .hide1, .hide2 {
    display:none;
  }
}

body {

	color: #000;
	margin: 0px;
	font-family: Verdana,Geneva,Lucida,'Lucida Grande',Arial,Helvetica,Sans-serif;
	font-size: 100%;
}
.row1{
 -ms-flex-wrap: wrap;
    flex-wrap: wrap;
    margin-right: -15px;
    margin-left: -15px;

}

.header, .content, .footer {
	display:block;
	clear: both;
	background: #fff;
	border: 1px solid #aebdc4;
	padding: 5px;
	margin: 5px;

	-moz-border-radius: 0.75em;
	-webkit-border-radius: 0.75em;
	border-radius: 0.75em;

/* Uncomment for fixed width layout:
	margin: 0 auto;
	width: 800px;
*/
}

text,	a:link, a:visited, a:active, a.CatLink, .CatDesc, .CatLockedName, .GenText,
	.GenTextRed, .ErrorText, .SmallText, .DateText, .TopBy, th, a.thLnk:link, a.thLnk:active, a.thLnk:visited,
	a.thLnk:active, .RowStyleA, .RowStyleB, .RowStyleC, .MsgToolBar, .PollTable, .MsgIg, td.miniMH,
	.ContentTable, .MsgTable, .DialogTable, .PreviewTable, .AnnonTable, .AnnText, td.NoAvatar, td.Av1, td.Av2, td.tabA,
	.AnnSubjText, a.PagerLink, .LevelText, .MsgSubText, .MsgBodyText, th.SelTS, th.SelFS, legend, td.tabH, div.tabT,
	.MiniTable, .quote, .dashed, .adminColor, .modsColor, td.tabI, td.tabA:hover,
	.brdrNormal, .brdrSelected, input.button, form, img, .StClr, .AnClr, td.msgot, td.msgud, div.ctags, td.mvTc, td.pmNav,
	.ModOpt, .MsgR1, .MsgR2, .MsgR3, .mnavMsg, .mnavWarnTbl, .mnavNoRes, font.lg, td.tt, table.tt, tr.tab, table.tab,
	a.big:link, a.big:active, a.big:visited, .small, a.small:link, a.small:active, a.small:visited, .curtime,
	font.anon, font.reg, td.permYES, td.permNO, td.repI, font.selmsgInd, fieldset
{
	font-family: Verdana,Geneva,Lucida,'Lucida Grande',Arial,Helvetica,Sans-serif;
	font-size: 10pt;
	line-height: 1.6;
}

a:link, a:visited, a:active, a.PagerLink {
	color: #069;
	text-decoration: underline;
}

a.CatLink {
	font-size: 10pt;
	font-weight: bold;
	text-decoration: none;
	color: #000;
}

.CatDesc {
	color: #000;
	height: 23px;
	background: url('/theme/default/images/tableHeaderbg2.png');
}

.CatLockedName {
	font-size: 10pt;
	font-weight: bold;
}

.CatLockPad {
	padding-left: 20px;
}

text, .GenText, .MsgIg, td.repI {
	color: #000;
}

.GenTextRed {
	color: red;
}

.ErrorText {
	color: red;
	font-weight: bold;
}

.SmallText, .DateText, .TopBy, td.permYES, td.permNO, .SmallText a {
	color: #202020;
	font-size: x-small;
}

th {
	font-size: 10pt;
	text-align: left;
	/*color: #fff*/;
	font-weight: bold;
	height: 23px;
	vertical-align: middle;
	background: url('/theme/default/images/tableHeaderbg.png');
}

a.thLnk:link, a.thLnk:active, a.thLnk:visited {
	font-size: 10pt;
	text-decoration: underline;
	color: #eee;
}

a.thLnk:visited {
	color: #c2cdd6;
}

.RowStyleA, td.tt {
	background: #eee;
}

.RowStyleB, .curtime, fieldset {
	background: #dee2e6;
}

.RowStyleC {
	background: #c2cdd6;
}

.row1:hover td {
	opacity: 0.8;
}

.MsgToolBar {
	background: #dee2e6;
	padding-left: 3px;
	padding-right: 3px;
}

.MsgSpacer {
	padding-bottom: 2px;
	padding-top: 2px;
}

.PollTable, .ContentTable, .MsgTable, .DialogTable, .PreviewTable, .AnnonTable {
	background: #fff;
	border: 0;
	padding: 0;
	margin: 0;
	clear: both;
}

font.ptp { /* space between poll title & number of votes */
	font-size: smaller;
	padding-left: 10px;
}

.ContentTable, .AnnonTable, .MsgTable, table.pad {
	width: 100%;
}

.MsgTable {
	margin-top: 0;
	margin-bottom: 0;
	border: 1px solid #888;
}

.DialogTable {
	width: 66%;
}

.PreviewTable {
	width: 75%;
}

.AnnText {
	color: #000;
	background: #f5d300;
	margin-top: 7px;
	margin-bottom: 7px;
}

.AnnSubjText {
	font-weight: bold;
}

.MsgSubText {
	font-weight: bold;
}

a.MsgSubText, a.MsgSubText:visited {
	color: black;
	text-decoration: none;
}

.MsgBodyText {
	font-size: 10pt;
	display: block;
	line-height: 1.7;
}

.FormattingToolsBG {
	padding-left: 5px;
	vertical-align: bottom;
	background: transparent;
}

.FormattingToolsCLR {
	-moz-border-radius: 0.15em;
	-webkit-border-radius: 0.15em;
	border-radius: 0.15em;

	padding: 1px;
	background: #fff;
}
.FormattingToolsCLR:hover {
	border: 1px; 
	background: #dee;
}

.MiniTable {
	background: #fff;
	border: 0;
	padding: 0;
	margin: 0;
}

cite {
	/* quote titles */
	color: #202020;
	font-size: 9pt;
	font-weight: bold;
	margin-left: 20px;
	display: block;
	background: transparent url('/theme/default/images/quote.gif') left top no-repeat;
	padding-left: 25px;
}

blockquote {
	/* quote tags */
	color: #444;
	background: #fafafa;
	border-left: 1px dashed #c2cdd6;
	margin: 8px 1px 0 20px;
	overflow: hidden;
	padding: 5px;
	font-style: italic;
}
blockquote:first-letter {
	margin: 0 .1em 0 -0.12em;
	font-family: ""Monotype Corsiva"", ""Apple Chancery"", fantasy;
	font-size: 160%;
	font-weight: bold;
	font-variant: small-caps;
}

blockquote blockquote {
	/* nested quote tags */
	background-color: #f0f0f0;
	margin: 8px 1px 0 10px;
}

blockquote blockquote blockquote {
	/* double nested quote tags */
	background-color: #e0e0e0;
}

.quote {
	/* old pre v3 quotes */
	color: #444444;
	background: #FAFAFA;
	border: 1px solid #c2cdd6;
}

.indent {
	/* [indent] & [tab] tags */
	margin-left: 35px;
}

.dashed {
	border: 1px dashed #1b7cad;
}

.adminColor {
	font-weight: bold;
	color: #f00;
}

.modsColor {
	color: #0b0;
	font-weight: bold;
}

.brdrNormal, td.tabItop, td.tabImid, td.tabIbot {
	border: #000;
	border-style: solid;
	border-left-width: 0;
	border-top-width: 0;
	border-right-width: 0;
	border-bottom-width: 1px;
	background-repeat: repeat;
	background-position: top;
}

.brdrSelected, td.tabAmid, td.tabAmid, td.tabAbot {
	border: #000;
	border-style: solid;
	border-left-width: 0;
	border-top-width: 0;
	border-right-width: 0;
	border-bottom-width: 0;
	background-repeat: repeat;
	background-position: top;
}

form {
	padding: 0;
	margin: 0;
}

img {
        border: 0;
        max-width: 800px;
        transition:all 0.5s ease-out;
}
img:hover {
    transform: scale(1.05);
}

figcaption {
        font-style: italic;
        font-variant: small-caps;
        font-size: smaller;
}

a[href^=""mailto:""] {
	background: url(/theme/default/images/email.png) no-repeat right center;
	padding-right:17px;
}

.StClr {
	font-weight: bold;
	color: green;
}

.AnClr {
	font-weight: bold;
	color: red;
}

.ModOpt, .ModOpt a {
	color: #a8b1b8;
	font-size: x-small;
	text-align: right;
}

.TopBy {
	margin-bottom: 0;
	padding-right: 2px;
	text-align: right;
}

.MsgR2, .MsgR1, .MsgR3, .MsgIg {
	background: #dee2e6;
	border-bottom: 1px solid #aaa;
	padding: 2px;
}

.MsgIg {
	border: 0;
}

.MsgR1 {
	background: #ccd4dc;
}

.MsgR3 {
	background: #f4f4f4;
	padding: 1ex;
	padding-top: 2ex;
}

.mnavMsg {
	border-bottom: 1px dashed #eee;
	width: 100%;
}

.mnavWarnTbl {
	border: 1px dashed red;
}

.mnavNoRes {
	border: 1px dashed blue;
}

font.lg {
	font-size: 10pt;
}

td.tt {
	text-align: left;
	width: 100%;
	border-bottom: 1px solid #fff;
}

table.tt {
	width: 100%;
	padding: 1px;
	margin: 0;
	text-align: center;
	border: 1px solid #000;
}

a.big:link, a.big:active, a.big:visited {
	color: #069;
	font-size: 11pt;
}

.small, a.small:link, a.small:active, a.small:visited {
	font-size: 9pt;
	font-weight: normal;
}

a.small:link, a.small:active, a.small:visited {
	color: #069;
}

a:hover, a.big:hover, td.msgud a:hover, td.msgot a:hover {
	color: #dc143c;
}

td.msgud, td.msgot, td.msgot a, div.ctags {
	color: #202020;
	font-size: 8pt;
	vertical-align: top;
	width: 33%;
	white-space: nowrap;
}

td.msgud {
	padding-right: 10px;
	text-align: left;
}

td.msgot {
	text-align: right;
}

div.ctags {
	padding: 2px;
	width: 100%;
}

legend {
	font-size: 11pt;
	font-weight: bold;
	background-color: #fff;
	border: 1px solid #aebdc4;
}

fieldset {
	border: 1px solid #aebdc4;
}

th.SelTS, th.SelFS {
	padding: 2px;
	color: #fff;
}

th.SelFS {
	 border-bottom: 1px solid #fff;
	 color: #fff;
}

td.NoAvatar {
	background: #f4f4f4;
	text-align: center;
}

td.Av1, td.Av2 {
	text-align: center;
	background: #f4f4f4;
	vertical-align: middle;
	white-space: nowrap;
}

td.Av2 {
	background: #fefefe;
}

td.tabI, td.tabA, td.tabI:hover, td.tabOVER, td.tabON {
	border: 1px solid black;
	background: #dee7f7;
        padding: 2px;
}

td.tabOVER, td.tabI:hover, td.tabI:active {
	background: #fff;
}

td.tabON {
	background: #5279bd;
}

a.tabON:link, a.tabON:active, a.tabON:visited, a.tabON:hover {
	color: #fff;
	font-weight: bold;
	text-decoration: none;
}

div.tabT {
	padding: 1px;
        padding-left: 10px;
        padding-right: 10px;
}

table.tab {
	border-bottom: 1px solid #000;
}

.curtime {
	border: 1px solid #aebdc4;
	vertical-align: middle;
	padding: 1px 3px 1px 3px;
}

td.pmSn {
	background: #fff;
}

td.pmSa {
	background: #fffa00;
}

td.pmSf {
	background: red;
}

table.pmDu {
	width: 175px;
	background-color: #fff;
	border: 1px #c2cdd6 solid;
}

td.mvTc {
	background: #e5ffe7;
}

td.pmNav {
	background: #fff;
	text-align: right;
}

td.miniMH {
	text-align: center;
}

tr.mnavH {
	background: #eee;
}

tr.mnavM {
	background: #dee2e6;
}

table.regConf {
	width: 80%;
	border: 1px dashed #f00;
}

font.anon, font.reg {
	color: #0a0;
}

td.permYES, td.permNO {
	text-align: center;
	color: red;
	white-space: nowrap;
}

td.permNO {
	color: blue;
}

font.regEW {
	font-size: xx-small;
	color: #050;
	font-weight: bold;
}

td.repI {
	padding: 5px;
	padding-left: 20px;
}

table.pad {
	margin-top: 2px;
	margin-bottom: 2px;
}

font.selmsgInd {
	font-size: xx-small;
	font-weight: bold;
	text-decoration: none;
}

td.avatarPad {
	padding-right: 3px;
	vertical-align: top;
}

div.pre {
	font-family: monospace;
	padding: 5px;
	border: 1px blue dashed;
	font-size: 10pt;
	white-space: pre;
	background: #fafafa;
	overflow: auto;
	margin: 5px;
}

/* Code formatting. */
pre { white-space: pre-wrap; }
.codehead, .codehead a { color: #202020; background: #e9e9e9; font-size: xx-small; font-weight:bold; }
html pre.prettyprint { border:none; padding: 0; margin: 5px;} 

a.big:active, a:active, a.CatLink:active, a.UserControlPanel:active, a.thLnk:active, 
a.small:active, a.PagerLink:active, a.MsgSubText:active {
	color: #dc143c;
}

/* BBcode [imgl] & [imgr] tags. */
img.l {
	float: left;
	clear: right;
	margin: 3px;
}

img.r {
	float: right;
	clear: left;
	margin: 3px;
}

.AttachmentsList {
	margin-left: 0;
	list-style: none;
	margin-top: 10px;
	padding: 0;
}

.signature {
	border-top: 1px dotted #a0a0a0;
	color: #202020;
	font-size: 8pt;
}

.header {color:#fff; background:#5478a4;}
.header a {color:#fff; text-decoration:none;}
.headtitle {padding: 10px; font-size:xx-large; font-weight:bold; text-decoration:none;}
.headdescr {padding: 20px 10px 0px 20px; color:#eee; font-size:small;}
.headsearch {padding: 0 0 10px 20px; font-size: small; font-weight:bold; float:right; white-space: nowrap;}

/* Make textareas's resizable (jQuery UI). */
.ui-resizable-s {
	height: 9px;
	background: #eee url(""/theme/default/images/grippie.gif"") no-repeat center 2px;
 	border: 1px solid #ddd;
	border-top-width: 0;
	overflow: hidden;
}
.ui-resizable-resizing textarea {
	opacity: 0.5;
	filter: alpha(opacity=50);
	zoom: 1;
}

/* The following are partial elements that are used to save space. */
.wo { width:		1px; }
.wa { width:		100%;
min-width: 900px;}
.ac { text-align:	center; }
.al { text-align:	left; }
.ar { text-align:	right; }
.fl { float: 		left; }
.fr { float: 		right; }
.vb { vertical-align:	bottom; }
.vt { vertical-align:	top; }
.vm { vertical-align:	middle; }
.nw { white-space: 	nowrap; }
.fb { font-weight:	bold; }
.dn { display: none; visibility:hidden;}
.clear { clear: both; }

img.at { float: left; clear: left; }

div.sr { float: left; padding: 5px; }

div.ctb {text-align: center; clear: both;}
div.ctb table {margin: 0 auto; text-align: left;}
div.ip { overflow: auto; margin: 0; padding: 2px; width: 600px; height: 400px; }
span.vt { padding-left: 20px; }
table.icqCP { font-family: arial, sans-serif; font-size: smaller; border: 0; }
div.pmL { padding-top: 2px; }
table.ucPW { width: 175px; }

/* Colors for highlighting search terms. */
.st0 { background-color: #ffff66; }
.st1 { background-color: #a0ffff; }
.st2 { background-color: #99ff99; }
.st3 { background-color: #ffd7a8; }
.st4 { background-color: #ff9999; }
.st5 { background-color: #aba8ff; }
.st6 { background-color: #b3ff66; }
.st7 { background-color: #fff2a8; }
.st8 { background-color: #a8d1ff; }
.st9 { background-color: #ffb7b7; }

/* User Control Panel / Top Level Menu. */
#UserControlPanel {
	float: right;
	width: 100%;
	margin: 5px 0 10px 10px;
}
#UserControlPanel ul {
	list-style: none;
	float: right;
	padding: 0;
	margin: 0;
	text-align:right;
}
#UserControlPanel li {
	margin: 0 7px 0 0;
	position:relative;
	display: inline;
	white-space: nowrap;
}
#UserControlPanel li a {
	color: #069;
	text-decoration: none;
}

/* Calendar. */
table.calendar {border-left:1px solid #999; }
tr.calendar-row1 {}
td.calendar-day {min-height:80px; font-size:11px; position:relative; } * html div.calendar-day { height:80px; }
td.calendar-day:hover {background:#eceff5; }
td.calendar-day-np {background:#eee; min-height:80px; } * html div.calendar-day-np { height:80px; }
td.calendar-day-head {background:#ccc; font-weight:bold; text-align:center; width:120px; padding:5px; border-bottom:1px solid #999; border-top:1px solid #999; border-right:1px solid #999; }
div.day-number {background:#999; position:absolute; z-index:2; top:-5px; right:-25px; padding:5px; color:#fff; font-weight:bold; width:20px; text-align:center; }
td.calendar-day, td.calendar-day-np { width:120px; padding:5px 25px 5px 5px; border-bottom:1px solid #999; border-right:1px solid #999; }

/* Buttons. */
input.button:link, input.button:visited, input.button {
	background:#ddd url(/theme/default/images/button.png) repeat-x scroll left top;
	border-color:#ccc #b4b2b4 #ccc #b4b2b4;
	position: relative;
	cursor: pointer;
	-moz-border-radius: 6px;
	-webkit-border-radius: 6px;
	-moz-box-shadow: 0 1px 3px rgba(0,0,0,0.6);
	-webkit-box-shadow: 0 1px 3px rgba(0,0,0,0.6);
}
input.button:hover, input.button:hover {
	background:#e2e2e2 url(/theme/default/images/button.png) repeat-x scroll left -115px;
	color:#000;
}
.small.button, .small.button:visited   { font-size: 11px; }

/* Karma voting */
a.karma {
    text-decoration: none;
}
a.up {
    color: green;
    font-weight: bold;
}
a.down {
    color: red;
    font-weight: bold;
}

/* Blog page - two unequal columns that floats next to each other */
/* Left column */
.leftcolumn {
    float: left;
    width: 74%;
}

/* Right column */
.rightcolumn {
    float: left;
    width: 24%;
    padding-left: 10px;
}

/* Add a card effect for articles */
.card {
    background-color: #f4f4f4;
    padding: 0 10px 10px 10px;
    margin-top: 10px;
    font-size: 10pt;
    line-height: 1.7;
    box-shadow: inset 0 0 10px #ccc;
}
.rightcolumn .card {
    background-color: #ddd;
}

/* Clear floats after the columns */
.group:after {
    content: """";
    display: table;
    clear: both;
}

/* Responsive layout - hide the sidebar when the screen is less than 768px wide. */
@media screen and (max-width: 768px) {
  .rightcolumn {
    display:none;
    width: 0%;
  }
  .leftcolumn {
    width: 98%;
    padding: 0;
  }
}


";
        }

        public class GridInfo
        {
            public string Title { get; set; }
            
            public string Who { get; set; }

            public int CountMessages { get; set; }

            public int CountViews { get; set; }

			public DateTime OnDate { get; set; }
        }

		//шаблон дизайна форума
		//string html = @"
		//    <div class='leftDiv'>@Who</div>            
		//    <div class='leftDiv' style='width:10px; height: 10px;'></div>
		//    <div class='leftDiv'>@OnDate</div>
		//    <br/>
		//    <div class='title'>@Title</div>
		//    <div class='picture'>@Picture</div>
		//    <br/>
		//    <div class='block'>
		//    <div class='text' tabindex='0'>@Text</div>
		//    <br/>
		//    <div  id='parent'>
		//        <table>
		//            <tr style='height:50px'>
		//                <td width='100%' nowrap>
		//                    <div class='newbutton black' onclick=""@ReadMore"">Read more</div>
		//                </td>
		//                <td nowrap valign='middle'>
		//                    <div  id='Comments'; >@Comments</div>
		//                </td>
		//                <td>
		//                    &nbsp;&nbsp;
		//                </td>
		//                <td nowrap valign='middle'>
		//                    <div id='like'>@Likes</div>
		//                </td>
		//            </tr>

		//        </table>
		//     </div>
		//    </div>
		//    <hr/>";

		string beginBody = @"<div class=""content"">
<table width=""100%""><tbody><tr><td>
<table border=""0"" cellspacing=""1"" cellpadding=""2"" class=""clear pad"">
<tbody><tr>
	<th class=""hide2"">&nbsp;</th>
	<th class=""hide2"">&nbsp;</th>
	<th class=""wa"">Тема</th>
	<th class=""wo hide1"">Ответы</th>
	<th class=""wo hide1"">Просмотров</th>
	<th class=""nw hide2"">Последнее сообщение</th>
</tr>";

        //string html = @"@Title<br>@Who<br>@CountMessages<br>@CountViews";
        string html = @"
<tr class=""row1"">
	<td class=""RowStyleB wo hide2""><img src=""@ReadImg"" title=""Отслеживание прочитанных и непрочитанных сообщений доступно только зарегистрированным участникам"" alt="""" width=""22"" height=""22""></td>
	<td class=""RowStyleB wo ac hide2"">&nbsp;</td>
	<td class=""RowStyleA""><a class=""big"" href=""@ReadMore"">@Title</a><br><span class=""small"">Ссылки на серию статей</span>   <div class=""TopBy"">От: <a href=""https://sqlru.net/index.php/u/53/"">@Who</a> - <span class=""DateText"">@OnDate</span></div></td>
	<td class=""RowStyleB ac hide1"">@CountMessages</td>
	<td class=""RowStyleB ac hide1"">@CountViews</td>
	<td class=""RowStyleC nw hide2""><span class=""DateText"">@OnDate</span><br>От: <a href=""https://sqlru.net/index.php/u/53/"">@Who</a> <a href=""https://sqlru.net/index.php/m/1162/#msg_1162""><img src=""./SQLRU.net_ Другие СУБД_files/goto.gif"" title=""Перейти к последнему сообщению в этой теме"" alt=""""></a></td>
</tr>
<tr><td><div  width: 5px; height: 5px;></div></td></tr>
";



        string endBody = @"
</tbody></table><table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""wa"">
<tbody><tr>
	<td class=""vt"">&nbsp;</td>
	<td class=""GenText nw vb ar""><a href=""https://sqlru.net/index.php/r/frm_id/14/""><img src=""@NewThreadImg"" alt=""Создать новую тему""></a></td>
</tr>
</tbody></table>
</td></tr></tbody></table>
</div>";
        //=========================

        string beginForumBody = @"<div class=""content"">
";
        string endForumBody = @"

</div>";


        string htmlForumContent = @"</tbody></table>

<table cellspacing=""0"" cellpadding=""0"" class=""ContentTable""><tbody><tr>
	<td class=""MsgSpacer"">
		<table cellspacing=""0"" cellpadding=""0"" class=""MsgTable"">
		<tbody><tr>
			<td class=""MsgR1 vt al expanded""><img src=""./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/min.png"" alt=""-"" title=""Свернуть сообщение"" class=""collapsable clickable"" style=""cursor: pointer;""> <a name=""msg_num_1""></a><a name=""msg_96""></a><span class=""MsgSubText""><a href=""https://sqlru.net/index.php/mv/msg/42/96/#msg_96"" class=""MsgSubText"">Полнотекстовый поиск для Firebird</a></span> <span class=""SmallText"">[<a href=""https://sqlru.net/index.php/mv/msg/42/96/#msg_96"">сообщение #96</a>]</span></td>
			<td class=""MsgR1 vt ar""><span class=""DateText"">Mon, 27 June 2022 09:39</span> <a href=""javascript://"" onclick=""chng_focus(&#39;#msg_num_2&#39;);""><img alt=""Переход к следующему сообщению"" title=""Переход к следующему сообщению"" src=""./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/down.png"" width=""16"" height=""11""></a></td>
		</tr>
		<tr class=""MsgR2"">
			<td class=""MsgR2"" colspan=""2"">
				<table cellspacing=""0"" cellpadding=""0"" class=""ContentTable"">
				<tbody><tr class=""MsgR2"">
				
					<td class=""msgud"">
						<img src=""./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/offline.png"" alt=""sim_84 в настоящее время не в онлайне"" title=""sim_84 в настоящее время не в онлайне"" width=""16"" height=""16"">&nbsp;
						<a href=""https://sqlru.net/index.php/u/21/"">sim_84</a>
						<br><b>Сообщений:</b> 129<br><b>Зарегистрирован:</b> June 2022 
						
					</td>
					<td class=""msgud""><div class=""ctags"">Senior Member</div></td>
					<td class=""msgot""></td>
		</tr>
		</tbody></table>
	</td>
</tr>
<tr>
	<td colspan=""2"" class=""MsgR3"">
		<span class=""MsgBodyText"">Хочу представить OpenSource UDR для полнотекстового поиска на основе Lucene++.<br>
<br>
<a href=""https://github.com/IBSurgeon/lucene_udr"" target=""_blank"" title=""Opens in a new window"">https://github.com/IBSurgeon/lucene_udr</a><br>
<br>
Документация на русском языке <a href=""https://github.com/IBSurgeon/lucene_udr/blob/main/README_RUS.md"" target=""_blank"" title=""Opens in a new window"">README_RUS.md</a><br>
<br>
UDR FTS Lucene написана на языке C++, обладает высокой производительностью. Может быть использована в Firebird 3.0 и 4.0.<br>
<br>
Пожелания по функционалу (адекватные) и сообщения об ошибках приветствуются.</span>
		
		
		<div class=""SmallText clear""><p class=""fl"">[Обновления: Tue, 28 June 2022 13:51]</p><p class=""fr""><a href=""https://sqlru.net/index.php/rm/96/"" rel=""nofollow"">Известить модератора</a></p></div>
</td></tr>
<tr>
	<td colspan=""2"" class=""MsgToolBar"">
		<table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""wa"">
		<tbody><tr>
			<td class=""al nw"">
				<a href=""https://sqlru.net/index.php/u/21/""><img alt="""" src=""./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/msg_about.gif""></a>&nbsp;<a href=""https://sqlru.net/index.php/pmm/toi/21/96/""><img alt=""Отправить личное сообщение этому участнику"" title=""Отправить личное сообщение этому участнику"" src=""./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/msg_pm.gif"" width=""71"" height=""18""></a>
				
			</td>
			<td class=""GenText wa ac"">&nbsp;</td>
			<td class=""nw ar"">
				
				
				<a href=""https://sqlru.net/index.php/r/reply_to/96/""><img alt="""" src=""./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/msg_reply.gif"" width=""71"" height=""18""></a>&nbsp;
				<a href=""https://sqlru.net/index.php/r/quote/true/96/""><img alt="""" src=""./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/msg_quote.gif"" width=""71"" height=""18""></a>
			</td>
		</tr>
		</tbody></table>";

		public class MessageInfo
        {
            public string Who { get; set; }

            public DateTime OnDate { get; set; }

            public string Avatar { get; set; }

            public string Message { get; set; }
        }

        public class TopicInfo
        {
            public string Title { get; set; }

            public string Who { get; set; }

            public uint CountMessages { get; set; }

            public uint CountViews { get; set; }

			public string Message { get; set; }

            public List<MessageInfo> Messages { get; set; }
        }

        public override string RenderControls(List<DOMControl> controls)
        {
            if (!DOMForm.IsAllDocs &&
                DOMForm.Name == "Topics")
            {
                var topic = DOMForm.Collection
                                   .GetDoc(DOMForm.DocID)
                                   .SelectOne<TopicInfo>();

                var sb = new StringBuilder();

				sb.AppendLine(beginForumBody);

				sb.Append(
                    "<table cellspacing=\"0\" cellpadding=\"0\" class=\"ContentTable\">" +
					"<tbody><tr>\r\n\t<td class=\"MsgSpacer\">\r\n\t\t" +
					"<table cellspacing=\"0\" cellpadding=\"0\" class=\"MsgTable\">\r\n" +
                    "<tbody><tr>" +
                    "<td class=\"MsgR1 vt al expanded\"><img src=\"./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/min.png\" alt=\"-\" title=\"Свернуть сообщение\" class=\"collapsable clickable\" style=\"cursor: pointer;\"> <a name=\"msg_num_1\"></a><a name=\"msg_96\"></a>" +
					"<span class=\"MsgSubText\">" +
                    "<a href=\"https://sqlru.net/index.php/mv/msg/42/96/#msg_96\" class=\"MsgSubText\">"+ topic.Title+"</a></span> <span class=\"SmallText\">[<a href=\"https://sqlru.net/index.php/mv/msg/42/96/#msg_96\">сообщение #96</a>]</span></td>" +
                    "<td class=\"MsgR1 vt ar\"><span class=\"DateText\">Mon, 27 June 2022 09:39</span> <a href=\"javascript://\" onclick=\"chng_focus(&#39;#msg_num_2&#39;);\"><img alt=\"Переход к следующему сообщению\" title=\"Переход к следующему сообщению\" src=\"./SQLRU.net_ Firebird, HQbird, InterBase » Полнотекстовый поиск для Firebird_files/down.png\" width=\"16\" height=\"11\"></a></td>\r\n" +
					"");

				sb.AppendLine("<br>" + topic.Title);
				sb.AppendLine("<br>" + topic.Who);
				sb.AppendLine("<br>" + topic.CountViews.ToString());
				sb.AppendLine("<br>" + topic.CountMessages.ToString());
				sb.AppendLine("<br>" + topic.Message);

				foreach(var message in topic.Messages) //3 messages
				{
                    sb.AppendLine("<br>================");
                    sb.AppendLine("<br>" + message.Who);
                    sb.AppendLine("<br>" + message.OnDate);
                    sb.AppendLine("<br>" + message.Avatar);
                    sb.AppendLine("<br>" + message.Message);
                }

                sb.AppendLine(endForumBody);

                return sb.ToString();
            }
            else
            {
                return base.RenderControls(controls);
            }
        }

        public override string RenderMainGrid(GridDOMControl domControl)
        {
            var sb = new StringBuilder();

            sb.Append("<td colspan=2>");

            var infos = DOMForm.Collection
                               .GetAll()
                               .Select<GridInfo>();
			sb.Append(beginBody);

			var index = 0;

			foreach (var info in infos)
			{
				var currHtml = html.Replace("@Title", info.Title); //тема
				currHtml = currHtml.Replace("@Who", info.Who); //от создателя
				currHtml = currHtml.Replace("@CountMessages", info.CountMessages.ToString()); //количество сообщений в теме
				currHtml = currHtml.Replace("@CountViews", info.CountViews.ToString()); //количество просмотров теми
                currHtml = currHtml.Replace("@OnDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm")); //
                currHtml = currHtml.Replace("@ReadMore", OnEditGridRowUrl(domControl, index));
                currHtml = currHtml.Replace("@ReadImg", GetFileUrl("Read.png"));
                
                sb.AppendLine(currHtml);

				index++;
			}

            endBody = endBody.Replace("@NewThreadImg", GetFileUrl("new_thread.gif"));

            sb.Append(endBody);

			sb.Append("</td>");

            return sb.ToString();
        }
    }
}
