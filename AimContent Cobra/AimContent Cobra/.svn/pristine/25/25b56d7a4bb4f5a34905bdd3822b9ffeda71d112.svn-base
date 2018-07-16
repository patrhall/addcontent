/*
Tree script. Includes Java script functions relayted to Tree view and file date 
and size format functions for Tree + Vista style sample.

You can edit and use this script in your applications which uses ActiveXPowUpload. 
*/
var tree = new dFTree({name: 'tree'});

var OPEN_NODE = "Open folder";
var CLOSE_NODE = "Close folder";

function OpenRootNode()
{
	if(tree._aNodes != undefined)
		tree._aNodes[tree._searchNode(0)].open();
}

function CloseRootNode()
{
	if(tree._aNodes != undefined)
		tree._aNodes[tree._searchNode(0)].close();
}

function ClickRow(id, event)
{	
	var Node=tree.getNodeById(id)		
	Node._select();
	
	showInfoForItem(id);
	
	if (!Node._io)
	{
		Node.open();	
	}	
}



function isNodeVisible(node)
{
	var i =0;
	if(node != null)
		if(node._parent != null)
			if(node._parent._io)
				return isNodeVisible(node._parent);
			else
				return false;
		else
			return 	true;
	return false;
}

function haveSelectedChild(node, checkClosed)
{
	var i = 0;	
	for(i = 0; i < node._children.length; i++)
	{
		if(node._children[i] == tree._selected)
		{
				return true;
		}
		else
		{
			if(node._children[i].haschild)		
			{
				if(checkClosed)
				{
					if(haveSelectedChild(node._children[i], checkClosed))
						return true;
					
				}
				else
				{
					if(node._children[i]._io)
						if(haveSelectedChild(node._children[i], checkClosed))
							return true;						
				}
			}
		}
	}
	return false;
}

function dNode(arrayProps) {
	var i;
	this.id;
	this.caption;
	this.folderpath;
	this.haschild=false;
	this._opened = false;
	this._io = false;

	this._children = [];
	this._parent;
	this._myTree;

	for (i in arrayProps)
	{
		if (i.charAt(0) != '_')
		{
			eval('this.'+i+' = arrayProps[\''+i+'\'];');
		}
	}
}

dNode.prototype.changeState = function()
{
	if (this._io)
	{
		this.close();
	}
	else
	{
		this.open();		
		//UpdateFolder(this.id)
	}
}

dNode.prototype.open = function () 
{
	if (!this._io)
	{
		this._opened = true;
		this._io = true;
		this._refresh();
		if(this != tree._selected)
			if(haveSelectedChild(this, false))
				showInfoForItem(tree._selected.id);
			else
				if(!haveSelectedChild(this, true) && isNodeVisible(tree._selected))
					showInfoForItem(tree._selected.id);
	}
}


dNode.prototype.close = function() 
{
	if (this._io)
	{
		this._io = false;
		this._refresh();
		if(this != tree._selected)
			if(haveSelectedChild(this, false))
				hideInfoPanel();
			else
				if(!haveSelectedChild(this, true) && isNodeVisible(tree._selected))
					showInfoForItem(tree._selected.id);

	}
}

dNode.prototype.alter = function(arrayProps)
{
	var i;
	for (i in arrayProps)
	{
		if (i != 'id' && i.charAt(0) != '_')
		{
			eval('this.'+i+' = arrayProps[\''+i+'\'];');
			if((i=='haschild') && arrayProps[i]==false)
			{
				eval('this._io = false;');
				eval('this._opened = false;');
			}
		}
	}
}

dNode.prototype._refresh = function() {
	var nodeDiv = document.getElementById("n"+this.id);
	var childrenDiv  = document.getElementById("ch"+this.id);
	var fnp = document.getElementById("fnp"+this.id);
	var fnc = document.getElementById("fnc"+this.id);
	var fnf = document.getElementById("fnf"+this.id);
	if (nodeDiv != null)
	{
		if (!this._io)
		{
			childrenDiv.className = "closed";
		}
		else 
		{
			if(this.haschild)
				childrenDiv.className = "opened";
		}		
		fnp.outerHTML = this._properPlus();			
		fnc.outerHTML = this._properCaption();		
		fnf.outerHTML = this._properFolder();
		nodeDiv.innerHTML = nodeDiv.innerHTML //IE refreshes DIV text only after this
	}

}

dNode.prototype._properCaption = function()
{
	if(this._myTree._selected==this)
		return '<font mytype="row" class="selectedItem" id="fnc'+this.id+'">' + this.caption + '</font>'
	else
		return '<font mytype="row" class="item" id="fnc'+this.id+'">' + this.caption + '</font>';
}

dNode.prototype._properPlus = function()
{
	if (!this._io) 
	{
		if(this.haschild)
			return '<font mytype="plus" id="fnp'+this.id+'"><img alt="' + OPEN_NODE + '" title="' + OPEN_NODE + '"  onclick="javascript: tree.getNodeById('+this.id+').changeState();" class="plus" mytype="plus" width="18" height="18" src="images/nolines_plus.gif"></font>'
		else
			return '<font mytype="plus" id="fnp'+this.id+'"><img src="images/spacer.gif" class="plus" width="18" height="18"></font>';
	}
	else 
	{
		if(this.haschild)
			return '<font mytype="plus" id="fnp'+this.id+'"><img alt="' + CLOSE_NODE + '" title="' + CLOSE_NODE + '"  onclick="javascript: tree.getNodeById('+this.id+').changeState();" class="plus" mytype="plus" width="18" height="18" src="images/nolines_minus.gif"></font>'
		else
			return '<font mytype="plus" id="fnp'+this.id+'"><img src="images/spacer.gif" class="plus" width="18" height="18"></font>';
	}
}

dNode.prototype._properFolder = function()
{
	if(this.id==0)
		return '<font mytype="row" id="fnf'+this.id+'"><img align="absmiddle" border="0" src="images/Vista_treeroot.gif" width="30" height="25"></font>'
	else
		if(this.haschild)
			if (this._io) 
				return '<font mytype="row" id="fnf'+this.id+'"><img align="absmiddle" border="0" src="images/Vista_foldertreeopen.gif" width="16" height="18"></font>';
			else
				return '<font mytype="row" id="fnf'+this.id+'"><img align="absmiddle" border="0" src="images/Vista_foldertreeclosed.gif" width="16" height="18"></font>';
		else
			return '<font mytype="row" id="fnf'+this.id+'"><img align="absmiddle" border="0" src="images/Vista_file.gif" width="16" height="18"></font>';
}

dNode.prototype._select = function()
{
	var captionSpan;
	if (this._myTree._selected)
	{
		this._myTree._selected._unselect();
	}
	this._myTree._selected = this;
	captionSpan  = document.getElementById("fnc"+this.id);
	if (captionSpan)
	{
		captionSpan.className = 'selectedItem';
	}

}

dNode.prototype._unselect = function()
{
	var captionSpan  = document.getElementById("fnc"+this.id);
	this._myTree._selected = null;
	if (captionSpan)
	{
		captionSpan.className = 'item';
	}
}

dNode.prototype._searchChild = function(id) 
{
	var a=0;
	for (a;a<this._children.length;a++)
	{
		if (this._children[a].id == id)
		{
			return a;
		}
	}
	return false;
}

dNode.prototype._draw = function()
{
	divN = document.createElement('div');
	divN.id = 'n'+this.id;
	divN.styleposition = 'absolute';
	
	if(this.id!=0)
		divN.className = 'son';

	spanP = document.createElement('span');
	spanP.id = 'p'+this.id;
	spanP.innerHTML='<span id="p'+this.id+'">'+this._properPlus()+'</span>';
	
	spanL = document.createElement('span');
	spanL.id = 'l'+this.id;
	spanL.innerHTML ='<span mytype="row" id="l'+this.id+'"  onclick="javascript: ClickRow('+ this.id + ', event);" ><nobr>'+ this._properFolder() + this._properCaption() + '</nobr></span>';

	divCH = document.createElement('div');
	divCH.id = 'ch'+this.id;
	divCH.className = (this._io)? "opened" : "closed";	

	divN.appendChild(spanP);
	divN.appendChild(spanL);
	divN.appendChild(divCH);	

	if (this._parent != null)
		parentChildrenDiv = document.getElementById("ch"+this._parent.id)
	else
		parentChildrenDiv = document.getElementById("dftree_"+this._myTree.name);
	if (parentChildrenDiv)
		parentChildrenDiv.appendChild(divN);

}

function dFTree(arrayProps) {
	var i;
	this.name;
	this._root;
	this._aNodes = [];
	this._selected;
	for (i in arrayProps)
	{
		if (i.charAt(0) != '_')
		{
			eval('this.'+i+' = arrayProps[\''+i+'\'];');
		}
	}
}

dFTree.prototype.draw = function() {
	if (!document.getElementById("dftree_"+this.name))
	{
		document.write('<div id="dftree_'+this.name+'"></div>');
	}

	if (this._root != null)
	{
		this._root._draw();
		this._drawBranch(this._root._children);
	}
}

dFTree.prototype.empty = function(id) {

	var Node = this._aNodes[this._searchNode(id)]
	var a=0;
	for (a;a<Node._children.length;a++)
	{
		this.empty(Node._children[a].id)		
	}
	if(Node._parent!=null)
	{
		Node._parent._children.splice(Node._parent._searchChild(id),1);
	}	
	NodPos = this._searchNode(id)
	this._aNodes.splice(NodPos,1)	
	clearnode(id)
	Node=null
}

dFTree.prototype.emptyChild = function(id)
{
	var Node = this._aNodes[this._searchNode(id)]
	var a=0;
	for (a;a<Node._children.length;a++)
	{
		this.empty(Node._children[a].id)		
	}
}

function clearnode(id)
{
	
	var divN = document.getElementById('n'+id);
	divN.parentNode.removeChild(divN);
	divN=null;
}
	
dFTree.prototype._drawBranch = function(childrenArray) 
{
	var a=0;
	for (a;a<childrenArray.length;a++)
	{
		childrenArray[a]._draw();
		this._drawBranch(childrenArray[a]._children);
	}
}

dFTree.prototype.add = function(node,pid) {
	var auxPos;
	var addNode = false;
	if (typeof (auxPos = this._searchNode(node.id)) != "number")
	{
		if (typeof (auxPos = this._searchNode(pid)) == "number")
		{			
			node._parent = this._aNodes[auxPos];
			this._aNodes[auxPos]._children[this._aNodes[auxPos]._children.length] = node;
			node._parent.haschild=true;
			if(node._parent.folderpath!='')
				node.folderpath=node._parent.folderpath + '\\' + node.caption
			else
				node.folderpath= node.caption;
			addNode = true;
		}
		else
		{
			if (this._root == null)
			{
				this._root = node;
				node.folderpath=''
				addNode = true;
			}
		}
		if (addNode)
		{
			this._aNodes[this._aNodes.length] = node;
			node._myTree = this;
			node._draw();
		}
	} 

}

dFTree.prototype.alter = function(arrayProps)
{
	if (arrayProps['id'])
	{
		this.getNodeById(arrayProps['id']).alter(arrayProps);
	}
}

dFTree.prototype.getNodeById = function(nodeid)
{
	return this._aNodes[this._searchNode(nodeid)];
}

dFTree.prototype._searchNode = function(id)
{
	var a=0;
	for (a;a<this._aNodes.length;a++)
	{
		if (this._aNodes[a].id == id)
		{
			return a;
		}
	}
	return false;
}

function replaceAll(string, search, replace)
{
	var ret = string;
	var startPos=0;	
	while(ret.indexOf(search, startPos) >= 0)	
	{		
		startPos = ret.indexOf(search, startPos)+1;
		var firstPart = ret.substr(0, startPos-1);
		var secondPart = ret.substr(startPos-1, ret.length-startPos+1);
		ret = firstPart+secondPart.replace(search,replace);							
	}
	return ret;
}

//return user friendly size string
function friendlySize(size)
{
	var sizeString = "";
	if(size<1024)
		sizeString = size+" bytes";
	else
	if(size>=1024 && size/1024<1024)
		sizeString = (Math.round((size/1024)*100)/100)+" KB";
	else
	if(size/1024>=1024 && size/1048576<1024)	
		sizeString = (Math.round((size/1048576)*100)/100)+" MB";
	else
	if(size/1048576>=1024 && size/1073741824<1024)
		sizeString = (Math.round((size/1073741824)*100)/100)+" GB";
	else
		sizeString = size;	
	return sizeString;
}


//return user friendly date string
function friendlyDate(date)
{		
	return dateFormat(new Date(date), "dd mmm yyyy HH:MM:ss");
}

var dateFormat = function (date, mask) 
{
	var	token        = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloZ]|"[^"]*"|'[^']*'/g,
		timezone     = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (value, length) {
			value = String(value);
			length = parseInt(length) || 2;
			while (value.length < length)
				value = "0" + value;
			return value;
		};

	// Regexes and supporting functions are cached through closure
	return function (date, mask) {
		// Treat the first argument as a mask if it doesn't contain any numbers
		if (
			arguments.length == 1 &&
			(typeof date == "string" || date instanceof String) &&
			!/\d/.test(date)
		) {
			mask = date;
			date = undefined;
		}

		date = date ? new Date(date) : new Date();
		if (isNaN(date))
			throw "invalid date";

		var dF = dateFormat;
		mask   = String(dF.masks[mask] || mask || dF.masks["default"]);

		var	d = date.getDate(),
			D = date.getDay(),
			m = date.getMonth(),
			y = date.getFullYear(),
			H = date.getHours(),
			M = date.getMinutes(),
			s = date.getSeconds(),
			L = date.getMilliseconds(),
			o = date.getTimezoneOffset(),
			flags = {
				d:    d,
				dd:   pad(d),
				ddd:  dF.i18n.dayNames[D],
				dddd: dF.i18n.dayNames[D + 7],
				m:    m + 1,
				mm:   pad(m + 1),
				mmm:  dF.i18n.monthNames[m],
				mmmm: dF.i18n.monthNames[m + 12],
				yy:   String(y).slice(2),
				yyyy: y,
				h:    H % 12 || 12,
				hh:   pad(H % 12 || 12),
				H:    H,
				HH:   pad(H),
				M:    M,
				MM:   pad(M),
				s:    s,
				ss:   pad(s),
				l:    pad(L, 3),
				L:    pad(L > 99 ? Math.round(L / 10) : L),
				t:    H < 12 ? "a"  : "p",
				tt:   H < 12 ? "am" : "pm",
				T:    H < 12 ? "A"  : "P",
				TT:   H < 12 ? "AM" : "PM",
				Z:    (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
				o:    (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4)
			};

		return mask.replace(token, function ($0) {
			return ($0 in flags) ? flags[$0] : $0.slice(1, $0.length - 1);
		});
	};
}();

// Some common format strings
dateFormat.masks = {
	"default":       "ddd mmm d yyyy HH:MM:ss",
	shortDate:       "m/d/yy",
	mediumDate:      "mmm d, yyyy",
	longDate:        "mmmm d, yyyy",
	fullDate:        "dddd, mmmm d, yyyy",
	shortTime:       "h:MM TT",
	mediumTime:      "h:MM:ss TT",
	longTime:        "h:MM:ss TT Z",
	isoDate:         "yyyy-mm-dd",
	isoTime:         "HH:MM:ss",
	isoDateTime:     "yyyy-mm-dd'T'HH:MM:ss",
	isoFullDateTime: "yyyy-mm-dd'T'HH:MM:ss.lo"
};

// Internationalization strings
dateFormat.i18n = {
	dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
	monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};