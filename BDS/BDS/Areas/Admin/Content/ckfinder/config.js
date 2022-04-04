/*
Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
For licensing, see license.txt or http://cksource.com/ckfinder/license
*/


CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.language = 'en';
    // config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/Areas/Admin/Content/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Areas/Admin/Content/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Areas/Admin/Content/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Flash';

};
};