mergeInto(LibraryManager.library, {
 //这里是代码1
  JSLog: function (str) {
  var strs=Pointer_stringify(str);
  //这个Log方法是前端那边写的方法
    Log(strs);
  },

	//这里可以添加若干个方法，方法之间记得用逗号隔开，
	//否则WebGL平台打包的时候会报错
});

