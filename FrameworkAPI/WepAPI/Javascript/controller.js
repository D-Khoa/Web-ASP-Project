/*Begin: Tab control*/
function OpenTab(evt, tabID){
	//Gọi danh sách các tab page
	var tabArray = document.getElementsByClassName("mes-tab-content");
	//Ẩn tất cả tab page tồn tại
	for(var i = 0; i < tabArray.length; i++){
		tabArray[i].className = tabArray[i].className.replace("show","");
	}
	//Gọi danh sách các tab header
	var tabActive = document.getElementsByClassName("mes-tab-control header");
	//Bỏ active tất cả tab header
	for(var i = 0; i < tabActive.length; i++){
		tabActive[i].className = tabActive[i].className.replace("active", "");
	}
	//Show tab page được chọn
	document.getElementById(tabID).className += " show";
	//Bật active tab header được chọn
	evt.currentTarget.className += " active";
}
/*End: Tab control*/