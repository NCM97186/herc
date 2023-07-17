/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	//config.uiColor = '#Fdb07e';
	config.removePlugins = 'Templates,About' ;
	config.removePlugins = 'elementspath';
	config.baseFloatZIndex = 100002;
	//config.FloatingPanelsZIndex = 10002;
   //config.skin = 'v2';
   //config.toolbar = 'Basic';
   
   CKEDITOR.config.toolbar = [
//   ['Source','-','Save','NewPage','Preview'],
//   ['Cut','Copy','Paste','PasteText','PasteFromWord','-','Print','SpellChecker','Scayt'],
//   ['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat']'
//   ['Form','Checkbox','Radio','TextField','Textarea','Select','Button','ImageButton','HiddenField'],
//   '/',
//   ['Bold','Italic','Underline','Strike','-','Subscript','Superscript'],
//   ['NumberedList','BulletedList','-','Outdent','Indent','Blockquote'],
//   ['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],
//   ['Link','Unlink','Anchor'],
//   ['Image','Flash','Table','HorizontalRule','Smiley','SpecialChar','PageBreak'],
//   '/',
//   ['Styles','Format','Font','FontSize'],['TextColor','BGColor'],
//   ['Maximize','ShowBlocks']];
  
   
   
//   ['Source','-','Save','NewPage','Preview','-','Undo','Redo','-',
//   'Cut','Copy','Paste','Find','Replace','-','SpellChecker','Scayt','Print','-',
//   'SelectAll','RemoveFormat'],
//   '/',
//   ['Bold','Italic','Underline','StrikeThrough','-',
//   'NumberedList','BulletedList','-','Outdent','Indent',
//   '-',
//   'JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock','-',
//   'Image','Table','-','Link','Flash','Smiley','PageBreak','SpecialChar'],
//   '/',
//   
//   ['Styles','Format','Font','FontSize','-',
//   'TextColor','BGColor','-']

['Bold','Italic','Underline','Font','FontSize','-','TextColor']
] ;

};
