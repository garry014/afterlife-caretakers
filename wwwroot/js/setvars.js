function setrbl(sname, rblname){
	if (localStorage.getItem(sname) != null || localStorage.getItem(sname) !== undefined || localStorage.getItem(sname) != ""){
	  var rbl = document.getElementsByName(rblname);
	  for (i=0; i<rbl.length; i++){
		if(rbl[i].value == localStorage.getItem(sname)){
			rbl[i].checked = true;
		}
	  }
	}
}

function setcb(sname, cbname){
	if (localStorage.getItem(sname) != null || localStorage.getItem(sname) !== undefined || localStorage.getItem(sname) != ""){
	  var cb = document.getElementById(cbname);
	  if (localStorage.getItem(sname) == "true"){
		cb.checked = true;
	  }
	  else{
		cb.checked = false;}
	}
}

function setddl(sname, ddlname){
	if (localStorage.getItem(sname) != null || localStorage.getItem(sname) !== undefined || localStorage.getItem(sname) != ""){
	  var ddl = document.getElementById(ddlname);
	  console.log(localStorage.getItem(sname));
	  ddl.value = localStorage.getItem(sname);
	}
}