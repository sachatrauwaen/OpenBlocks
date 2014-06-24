/*********************************************************************************************************/
/**
 * Ventrian News Articles Article Link Selector plugin for CKEditor by Ingo Herbote
 * 
 */
/*********************************************************************************************************/

( function() {
    CKEDITOR.plugins.add( 'dnntokens',
    {
        requires: [ 'iframedialog' ],
		lang: ['de', 'en', 'pl'],
        init: function( editor )
        {
           var me = this;
		   
           CKEDITOR.dialog.add( 'dnntokensDialog', function (editor)
           {
               return {
                  
                 title: editor.lang.dnntokens.title,
                 minWidth : 550,
                 minHeight : 400,
                 contents :
                       [
                          {
                             id : 'iframe',
                             label : 'DNN Tokens',
                             expand : true,
                             elements :
                                   [
                                      {
						               type : 'html',
						               id : 'pagednntokens',
						               label : 'DNN Tokens',
						               style : 'width : 100%;',
						               html: '<iframe src="' + me.path + '/dialogs/dnntokens.aspx" frameborder="0" name="iframednntokens" id="iframednntokens" allowtransparency="1" style="width:100%;margin:0;padding:0;height:400px;"></iframe>'
						              }
                                   ]
                          }
                       ],
                 onOk : function()
                 {
					for (var i=0; i<window.frames.length; i++) {
					    if (window.frames[i].name == 'iframednntokens') {

					        //window.frames[i].CallServer("", "");
					        
					        window.EditorCloseCallBack = function (Token) {
					            //alert(Token);
					            //var Token = window.frames[i].document.getElementById("hfToken").value;
					            editor.insertHtml(Token);
					            CKEDITOR.dialog.getCurrent().hide();
					        }
					        window.frames[i].EditorClose();
					        
                            /*
					        window.frames[i].myfunction(function () {
					            alert('myfunction2');
					        });
                            */
					      //var Token = window.frames[i].document.getElementById("hfToken").value;
					      //editor.insertHtml(Token);
					      return false;
					   }
					}
			        
                 }
              };
           } );

            editor.addCommand( 'dnntokens', new CKEDITOR.dialogCommand( 'dnntokensDialog' ) );

            editor.ui.addButton( 'dnntokens',
            {
                label: editor.lang.dnntokens.button,
                command: 'dnntokens',
                icon: this.path + 'icon.gif'
            } );
        }
    } );
} )();
