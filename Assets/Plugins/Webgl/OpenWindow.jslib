var OpenWindowPlugin = {
    openWindow: function(link)
    {
        var url = Pointer_stringify(link);
        document.onmouseup = function()
        {
            window.open(url);
            document.onmouseup = null;
        }
		
		 document.ontouchstart = function()
        {
            window.open(url);
            document.ontouchstart = null;
        }
    }
};
 
mergeInto(LibraryManager.library, OpenWindowPlugin);