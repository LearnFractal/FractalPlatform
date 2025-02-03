(function(){'use strict';var r;function aa(a){var b=0;return function(){return b<a.length?{done:!1,value:a[b++]}:{done:!0}}}
var ba=typeof Object.defineProperties=="function"?Object.defineProperty:function(a,b,c){if(a==Array.prototype||a==Object.prototype)return a;a[b]=c.value;return a};
function ca(a){a=["object"==typeof globalThis&&globalThis,a,"object"==typeof window&&window,"object"==typeof self&&self,"object"==typeof global&&global];for(var b=0;b<a.length;++b){var c=a[b];if(c&&c.Math==Math)return c}throw Error("Cannot find global object");}
var fa=ca(this);function v(a,b){if(b)a:{var c=fa;a=a.split(".");for(var d=0;d<a.length-1;d++){var e=a[d];if(!(e in c))break a;c=c[e]}a=a[a.length-1];d=c[a];b=b(d);b!=d&&b!=null&&ba(c,a,{configurable:!0,writable:!0,value:b})}}
v("Symbol",function(a){function b(f){if(this instanceof b)throw new TypeError("Symbol is not a constructor");return new c(d+(f||"")+"_"+e++,f)}
function c(f,g){this.h=f;ba(this,"description",{configurable:!0,writable:!0,value:g})}
if(a)return a;c.prototype.toString=function(){return this.h};
var d="jscomp_symbol_"+(Math.random()*1E9>>>0)+"_",e=0;return b});
v("Symbol.iterator",function(a){if(a)return a;a=Symbol("Symbol.iterator");for(var b="Array Int8Array Uint8Array Uint8ClampedArray Int16Array Uint16Array Int32Array Uint32Array Float32Array Float64Array".split(" "),c=0;c<b.length;c++){var d=fa[b[c]];typeof d==="function"&&typeof d.prototype[a]!="function"&&ba(d.prototype,a,{configurable:!0,writable:!0,value:function(){return ha(aa(this))}})}return a});
function ha(a){a={next:a};a[Symbol.iterator]=function(){return this};
return a}
var ia=typeof Object.create=="function"?Object.create:function(a){function b(){}
b.prototype=a;return new b},la=function(){function a(){function c(){}
new c;Reflect.construct(c,[],function(){});
return new c instanceof c}
if(typeof Reflect!="undefined"&&Reflect.construct){if(a())return Reflect.construct;var b=Reflect.construct;return function(c,d,e){c=b(c,d);e&&Reflect.setPrototypeOf(c,e.prototype);return c}}return function(c,d,e){e===void 0&&(e=c);
e=ia(e.prototype||Object.prototype);return Function.prototype.apply.call(c,e,d)||e}}(),ma;
if(typeof Object.setPrototypeOf=="function")ma=Object.setPrototypeOf;else{var na;a:{var oa={a:!0},pa={};try{pa.__proto__=oa;na=pa.a;break a}catch(a){}na=!1}ma=na?function(a,b){a.__proto__=b;if(a.__proto__!==b)throw new TypeError(a+" is not extensible");return a}:null}var qa=ma;
function w(a,b){a.prototype=ia(b.prototype);a.prototype.constructor=a;if(qa)qa(a,b);else for(var c in b)if(c!="prototype")if(Object.defineProperties){var d=Object.getOwnPropertyDescriptor(b,c);d&&Object.defineProperty(a,c,d)}else a[c]=b[c];a.Aa=b.prototype}
function y(a){var b=typeof Symbol!="undefined"&&Symbol.iterator&&a[Symbol.iterator];if(b)return b.call(a);if(typeof a.length=="number")return{next:aa(a)};throw Error(String(a)+" is not an iterable or ArrayLike");}
function ra(a){if(!(a instanceof Array)){a=y(a);for(var b,c=[];!(b=a.next()).done;)c.push(b.value);a=c}return a}
function sa(a){return ta(a,a)}
function ta(a,b){a.raw=b;Object.freeze&&(Object.freeze(a),Object.freeze(b));return a}
function ua(a,b){return Object.prototype.hasOwnProperty.call(a,b)}
var va=typeof Object.assign=="function"?Object.assign:function(a,b){for(var c=1;c<arguments.length;c++){var d=arguments[c];if(d)for(var e in d)ua(d,e)&&(a[e]=d[e])}return a};
v("Object.assign",function(a){return a||va});
function wa(){this.D=!1;this.u=null;this.i=void 0;this.h=1;this.o=this.M=0;this.P=this.j=null}
function xa(a){if(a.D)throw new TypeError("Generator is already running");a.D=!0}
wa.prototype.G=function(a){this.i=a};
function ya(a,b){a.j={exception:b,yd:!0};a.h=a.M||a.o}
wa.prototype.return=function(a){this.j={return:a};this.h=this.o};
wa.prototype.yield=function(a,b){this.h=b;return{value:a}};
wa.prototype.A=function(a){this.h=a};
function za(a,b,c){a.M=b;c!=void 0&&(a.o=c)}
function Aa(a,b){a.h=b;a.M=0}
function Ba(a){a.M=0;var b=a.j.exception;a.j=null;return b}
function Ca(a){var b=a.P.splice(0)[0];(b=a.j=a.j||b)?b.yd?a.h=a.M||a.o:b.A!=void 0&&a.o<b.A?(a.h=b.A,a.j=null):a.h=a.o:a.h=0}
function Da(a){this.h=new wa;this.i=a}
function Ea(a,b){xa(a.h);var c=a.h.u;if(c)return Fa(a,"return"in c?c["return"]:function(d){return{value:d,done:!0}},b,a.h.return);
a.h.return(b);return Ga(a)}
function Fa(a,b,c,d){try{var e=b.call(a.h.u,c);if(!(e instanceof Object))throw new TypeError("Iterator result "+e+" is not an object");if(!e.done)return a.h.D=!1,e;var f=e.value}catch(g){return a.h.u=null,ya(a.h,g),Ga(a)}a.h.u=null;d.call(a.h,f);return Ga(a)}
function Ga(a){for(;a.h.h;)try{var b=a.i(a.h);if(b)return a.h.D=!1,{value:b.value,done:!1}}catch(c){a.h.i=void 0,ya(a.h,c)}a.h.D=!1;if(a.h.j){b=a.h.j;a.h.j=null;if(b.yd)throw b.exception;return{value:b.return,done:!0}}return{value:void 0,done:!0}}
function Ha(a){this.next=function(b){xa(a.h);a.h.u?b=Fa(a,a.h.u.next,b,a.h.G):(a.h.G(b),b=Ga(a));return b};
this.throw=function(b){xa(a.h);a.h.u?b=Fa(a,a.h.u["throw"],b,a.h.G):(ya(a.h,b),b=Ga(a));return b};
this.return=function(b){return Ea(a,b)};
this[Symbol.iterator]=function(){return this}}
function Ia(a){function b(d){return a.next(d)}
function c(d){return a.throw(d)}
return new Promise(function(d,e){function f(g){g.done?d(g.value):Promise.resolve(g.value).then(b,c).then(f,e)}
f(a.next())})}
function A(a){return Ia(new Ha(new Da(a)))}
function B(){for(var a=Number(this),b=[],c=a;c<arguments.length;c++)b[c-a]=arguments[c];return b}
v("globalThis",function(a){return a||fa});
v("Reflect",function(a){return a?a:{}});
v("Reflect.construct",function(){return la});
v("Reflect.setPrototypeOf",function(a){return a?a:qa?function(b,c){try{return qa(b,c),!0}catch(d){return!1}}:null});
v("Promise",function(a){function b(g){this.X=0;this.ab=void 0;this.h=[];this.u=!1;var h=this.i();try{g(h.resolve,h.reject)}catch(k){h.reject(k)}}
function c(){this.h=null}
function d(g){return g instanceof b?g:new b(function(h){h(g)})}
if(a)return a;c.prototype.i=function(g){if(this.h==null){this.h=[];var h=this;this.j(function(){h.u()})}this.h.push(g)};
var e=fa.setTimeout;c.prototype.j=function(g){e(g,0)};
c.prototype.u=function(){for(;this.h&&this.h.length;){var g=this.h;this.h=[];for(var h=0;h<g.length;++h){var k=g[h];g[h]=null;try{k()}catch(l){this.o(l)}}}this.h=null};
c.prototype.o=function(g){this.j(function(){throw g;})};
b.prototype.i=function(){function g(l){return function(m){k||(k=!0,l.call(h,m))}}
var h=this,k=!1;return{resolve:g(this.U),reject:g(this.j)}};
b.prototype.U=function(g){if(g===this)this.j(new TypeError("A Promise cannot resolve to itself"));else if(g instanceof b)this.Z(g);else{a:switch(typeof g){case "object":var h=g!=null;break a;case "function":h=!0;break a;default:h=!1}h?this.P(g):this.o(g)}};
b.prototype.P=function(g){var h=void 0;try{h=g.then}catch(k){this.j(k);return}typeof h=="function"?this.ha(h,g):this.o(g)};
b.prototype.j=function(g){this.M(2,g)};
b.prototype.o=function(g){this.M(1,g)};
b.prototype.M=function(g,h){if(this.X!=0)throw Error("Cannot settle("+g+", "+h+"): Promise already settled in state"+this.X);this.X=g;this.ab=h;this.X===2&&this.Y();this.D()};
b.prototype.Y=function(){var g=this;e(function(){if(g.G()){var h=fa.console;typeof h!=="undefined"&&h.error(g.ab)}},1)};
b.prototype.G=function(){if(this.u)return!1;var g=fa.CustomEvent,h=fa.Event,k=fa.dispatchEvent;if(typeof k==="undefined")return!0;typeof g==="function"?g=new g("unhandledrejection",{cancelable:!0}):typeof h==="function"?g=new h("unhandledrejection",{cancelable:!0}):(g=fa.document.createEvent("CustomEvent"),g.initCustomEvent("unhandledrejection",!1,!0,g));g.promise=this;g.reason=this.ab;return k(g)};
b.prototype.D=function(){if(this.h!=null){for(var g=0;g<this.h.length;++g)f.i(this.h[g]);this.h=null}};
var f=new c;b.prototype.Z=function(g){var h=this.i();g.fc(h.resolve,h.reject)};
b.prototype.ha=function(g,h){var k=this.i();try{g.call(h,k.resolve,k.reject)}catch(l){k.reject(l)}};
b.prototype.then=function(g,h){function k(p,t){return typeof p=="function"?function(u){try{l(p(u))}catch(x){m(x)}}:t}
var l,m,n=new b(function(p,t){l=p;m=t});
this.fc(k(g,l),k(h,m));return n};
b.prototype.catch=function(g){return this.then(void 0,g)};
b.prototype.fc=function(g,h){function k(){switch(l.X){case 1:g(l.ab);break;case 2:h(l.ab);break;default:throw Error("Unexpected state: "+l.X);}}
var l=this;this.h==null?f.i(k):this.h.push(k);this.u=!0};
b.resolve=d;b.reject=function(g){return new b(function(h,k){k(g)})};
b.race=function(g){return new b(function(h,k){for(var l=y(g),m=l.next();!m.done;m=l.next())d(m.value).fc(h,k)})};
b.all=function(g){var h=y(g),k=h.next();return k.done?d([]):new b(function(l,m){function n(u){return function(x){p[u]=x;t--;t==0&&l(p)}}
var p=[],t=0;do p.push(void 0),t++,d(k.value).fc(n(p.length-1),m),k=h.next();while(!k.done)})};
return b});
v("Object.setPrototypeOf",function(a){return a||qa});
v("Symbol.dispose",function(a){return a?a:Symbol("Symbol.dispose")});
v("WeakMap",function(a){function b(k){this.h=(h+=Math.random()+1).toString();if(k){k=y(k);for(var l;!(l=k.next()).done;)l=l.value,this.set(l[0],l[1])}}
function c(){}
function d(k){var l=typeof k;return l==="object"&&k!==null||l==="function"}
function e(k){if(!ua(k,g)){var l=new c;ba(k,g,{value:l})}}
function f(k){var l=Object[k];l&&(Object[k]=function(m){if(m instanceof c)return m;Object.isExtensible(m)&&e(m);return l(m)})}
if(function(){if(!a||!Object.seal)return!1;try{var k=Object.seal({}),l=Object.seal({}),m=new a([[k,2],[l,3]]);if(m.get(k)!=2||m.get(l)!=3)return!1;m.delete(k);m.set(l,4);return!m.has(k)&&m.get(l)==4}catch(n){return!1}}())return a;
var g="$jscomp_hidden_"+Math.random();f("freeze");f("preventExtensions");f("seal");var h=0;b.prototype.set=function(k,l){if(!d(k))throw Error("Invalid WeakMap key");e(k);if(!ua(k,g))throw Error("WeakMap key fail: "+k);k[g][this.h]=l;return this};
b.prototype.get=function(k){return d(k)&&ua(k,g)?k[g][this.h]:void 0};
b.prototype.has=function(k){return d(k)&&ua(k,g)&&ua(k[g],this.h)};
b.prototype.delete=function(k){return d(k)&&ua(k,g)&&ua(k[g],this.h)?delete k[g][this.h]:!1};
return b});
v("Map",function(a){function b(){var h={};return h.previous=h.next=h.head=h}
function c(h,k){var l=h[1];return ha(function(){if(l){for(;l.head!=h[1];)l=l.previous;for(;l.next!=l.head;)return l=l.next,{done:!1,value:k(l)};l=null}return{done:!0,value:void 0}})}
function d(h,k){var l=k&&typeof k;l=="object"||l=="function"?f.has(k)?l=f.get(k):(l=""+ ++g,f.set(k,l)):l="p_"+k;var m=h[0][l];if(m&&ua(h[0],l))for(h=0;h<m.length;h++){var n=m[h];if(k!==k&&n.key!==n.key||k===n.key)return{id:l,list:m,index:h,entry:n}}return{id:l,list:m,index:-1,entry:void 0}}
function e(h){this[0]={};this[1]=b();this.size=0;if(h){h=y(h);for(var k;!(k=h.next()).done;)k=k.value,this.set(k[0],k[1])}}
if(function(){if(!a||typeof a!="function"||!a.prototype.entries||typeof Object.seal!="function")return!1;try{var h=Object.seal({x:4}),k=new a(y([[h,"s"]]));if(k.get(h)!="s"||k.size!=1||k.get({x:4})||k.set({x:4},"t")!=k||k.size!=2)return!1;var l=k.entries(),m=l.next();if(m.done||m.value[0]!=h||m.value[1]!="s")return!1;m=l.next();return m.done||m.value[0].x!=4||m.value[1]!="t"||!l.next().done?!1:!0}catch(n){return!1}}())return a;
var f=new WeakMap;e.prototype.set=function(h,k){h=h===0?0:h;var l=d(this,h);l.list||(l.list=this[0][l.id]=[]);l.entry?l.entry.value=k:(l.entry={next:this[1],previous:this[1].previous,head:this[1],key:h,value:k},l.list.push(l.entry),this[1].previous.next=l.entry,this[1].previous=l.entry,this.size++);return this};
e.prototype.delete=function(h){h=d(this,h);return h.entry&&h.list?(h.list.splice(h.index,1),h.list.length||delete this[0][h.id],h.entry.previous.next=h.entry.next,h.entry.next.previous=h.entry.previous,h.entry.head=null,this.size--,!0):!1};
e.prototype.clear=function(){this[0]={};this[1]=this[1].previous=b();this.size=0};
e.prototype.has=function(h){return!!d(this,h).entry};
e.prototype.get=function(h){return(h=d(this,h).entry)&&h.value};
e.prototype.entries=function(){return c(this,function(h){return[h.key,h.value]})};
e.prototype.keys=function(){return c(this,function(h){return h.key})};
e.prototype.values=function(){return c(this,function(h){return h.value})};
e.prototype.forEach=function(h,k){for(var l=this.entries(),m;!(m=l.next()).done;)m=m.value,h.call(k,m[1],m[0],this)};
e.prototype[Symbol.iterator]=e.prototype.entries;var g=0;return e});
v("Set",function(a){function b(c){this.h=new Map;if(c){c=y(c);for(var d;!(d=c.next()).done;)this.add(d.value)}this.size=this.h.size}
if(function(){if(!a||typeof a!="function"||!a.prototype.entries||typeof Object.seal!="function")return!1;try{var c=Object.seal({x:4}),d=new a(y([c]));if(!d.has(c)||d.size!=1||d.add(c)!=d||d.size!=1||d.add({x:4})!=d||d.size!=2)return!1;var e=d.entries(),f=e.next();if(f.done||f.value[0]!=c||f.value[1]!=c)return!1;f=e.next();return f.done||f.value[0]==c||f.value[0].x!=4||f.value[1]!=f.value[0]?!1:e.next().done}catch(g){return!1}}())return a;
b.prototype.add=function(c){c=c===0?0:c;this.h.set(c,c);this.size=this.h.size;return this};
b.prototype.delete=function(c){c=this.h.delete(c);this.size=this.h.size;return c};
b.prototype.clear=function(){this.h.clear();this.size=0};
b.prototype.has=function(c){return this.h.has(c)};
b.prototype.entries=function(){return this.h.entries()};
b.prototype.values=function(){return this.h.values()};
b.prototype.keys=b.prototype.values;b.prototype[Symbol.iterator]=b.prototype.values;b.prototype.forEach=function(c,d){var e=this;this.h.forEach(function(f){return c.call(d,f,f,e)})};
return b});
function Ja(a,b){a instanceof String&&(a+="");var c=0,d=!1,e={next:function(){if(!d&&c<a.length){var f=c++;return{value:b(f,a[f]),done:!1}}d=!0;return{done:!0,value:void 0}}};
e[Symbol.iterator]=function(){return e};
return e}
v("Array.prototype.entries",function(a){return a?a:function(){return Ja(this,function(b,c){return[b,c]})}});
v("Array.prototype.keys",function(a){return a?a:function(){return Ja(this,function(b){return b})}});
function Ka(a,b,c){if(a==null)throw new TypeError("The 'this' value for String.prototype."+c+" must not be null or undefined");if(b instanceof RegExp)throw new TypeError("First argument to String.prototype."+c+" must not be a regular expression");return a+""}
v("String.prototype.startsWith",function(a){return a?a:function(b,c){var d=Ka(this,b,"startsWith");b+="";var e=d.length,f=b.length;c=Math.max(0,Math.min(c|0,d.length));for(var g=0;g<f&&c<e;)if(d[c++]!=b[g++])return!1;return g>=f}});
v("String.prototype.endsWith",function(a){return a?a:function(b,c){var d=Ka(this,b,"endsWith");b+="";c===void 0&&(c=d.length);c=Math.max(0,Math.min(c|0,d.length));for(var e=b.length;e>0&&c>0;)if(d[--c]!=b[--e])return!1;return e<=0}});
v("Number.isFinite",function(a){return a?a:function(b){return typeof b!=="number"?!1:!isNaN(b)&&b!==Infinity&&b!==-Infinity}});
v("Array.prototype.find",function(a){return a?a:function(b,c){a:{var d=this;d instanceof String&&(d=String(d));for(var e=d.length,f=0;f<e;f++){var g=d[f];if(b.call(c,g,f,d)){b=g;break a}}b=void 0}return b}});
v("Object.values",function(a){return a?a:function(b){var c=[],d;for(d in b)ua(b,d)&&c.push(b[d]);return c}});
v("Object.is",function(a){return a?a:function(b,c){return b===c?b!==0||1/b===1/c:b!==b&&c!==c}});
v("Array.prototype.includes",function(a){return a?a:function(b,c){var d=this;d instanceof String&&(d=String(d));var e=d.length;c=c||0;for(c<0&&(c=Math.max(c+e,0));c<e;c++){var f=d[c];if(f===b||Object.is(f,b))return!0}return!1}});
v("String.prototype.includes",function(a){return a?a:function(b,c){return Ka(this,b,"includes").indexOf(b,c||0)!==-1}});
v("Array.from",function(a){return a?a:function(b,c,d){c=c!=null?c:function(h){return h};
var e=[],f=typeof Symbol!="undefined"&&Symbol.iterator&&b[Symbol.iterator];if(typeof f=="function"){b=f.call(b);for(var g=0;!(f=b.next()).done;)e.push(c.call(d,f.value,g++))}else for(f=b.length,g=0;g<f;g++)e.push(c.call(d,b[g],g));return e}});
v("Object.entries",function(a){return a?a:function(b){var c=[],d;for(d in b)ua(b,d)&&c.push([d,b[d]]);return c}});
v("Number.MAX_SAFE_INTEGER",function(){return 9007199254740991});
v("Number.MIN_SAFE_INTEGER",function(){return-9007199254740991});
v("Number.isInteger",function(a){return a?a:function(b){return Number.isFinite(b)?b===Math.floor(b):!1}});
v("Number.isSafeInteger",function(a){return a?a:function(b){return Number.isInteger(b)&&Math.abs(b)<=Number.MAX_SAFE_INTEGER}});
v("Math.trunc",function(a){return a?a:function(b){b=Number(b);if(isNaN(b)||b===Infinity||b===-Infinity||b===0)return b;var c=Math.floor(Math.abs(b));return b<0?-c:c}});
v("Number.isNaN",function(a){return a?a:function(b){return typeof b==="number"&&isNaN(b)}});
v("Array.prototype.values",function(a){return a?a:function(){return Ja(this,function(b,c){return c})}});
v("Math.clz32",function(a){return a?a:function(b){b=Number(b)>>>0;if(b===0)return 32;var c=0;(b&4294901760)===0&&(b<<=16,c+=16);(b&4278190080)===0&&(b<<=8,c+=8);(b&4026531840)===0&&(b<<=4,c+=4);(b&3221225472)===0&&(b<<=2,c+=2);(b&2147483648)===0&&c++;return c}});
v("Math.log10",function(a){return a?a:function(b){return Math.log(b)/Math.LN10}});/*

 Copyright The Closure Library Authors.
 SPDX-License-Identifier: Apache-2.0
*/
var La=La||{},C=this||self;function D(a,b,c){a=a.split(".");c=c||C;a[0]in c||typeof c.execScript=="undefined"||c.execScript("var "+a[0]);for(var d;a.length&&(d=a.shift());)a.length||b===void 0?c[d]&&c[d]!==Object.prototype[d]?c=c[d]:c=c[d]={}:c[d]=b}
function E(a,b){a=a.split(".");b=b||C;for(var c=0;c<a.length;c++)if(b=b[a[c]],b==null)return null;return b}
function Ma(a){var b=typeof a;return b!="object"?b:a?Array.isArray(a)?"array":b:"null"}
function Na(a){var b=Ma(a);return b=="array"||b=="object"&&typeof a.length=="number"}
function Sa(a){var b=typeof a;return b=="object"&&a!=null||b=="function"}
function Ta(a){return Object.prototype.hasOwnProperty.call(a,Ua)&&a[Ua]||(a[Ua]=++Va)}
var Ua="closure_uid_"+(Math.random()*1E9>>>0),Va=0;function Wa(a,b,c){return a.call.apply(a.bind,arguments)}
function Xa(a,b,c){if(!a)throw Error();if(arguments.length>2){var d=Array.prototype.slice.call(arguments,2);return function(){var e=Array.prototype.slice.call(arguments);Array.prototype.unshift.apply(e,d);return a.apply(b,e)}}return function(){return a.apply(b,arguments)}}
function Za(a,b,c){Za=Function.prototype.bind&&Function.prototype.bind.toString().indexOf("native code")!=-1?Wa:Xa;return Za.apply(null,arguments)}
function $a(a,b){var c=Array.prototype.slice.call(arguments,1);return function(){var d=c.slice();d.push.apply(d,arguments);return a.apply(this,d)}}
function ab(){return Date.now()}
function bb(a){return a}
function cb(a,b){function c(){}
c.prototype=b.prototype;a.Aa=b.prototype;a.prototype=new c;a.prototype.constructor=a;a.base=function(d,e,f){for(var g=Array(arguments.length-2),h=2;h<arguments.length;h++)g[h-2]=arguments[h];return b.prototype[e].apply(d,g)}}
;function db(a,b){if(Error.captureStackTrace)Error.captureStackTrace(this,db);else{var c=Error().stack;c&&(this.stack=c)}a&&(this.message=String(a));b!==void 0&&(this.cause=b)}
cb(db,Error);db.prototype.name="CustomError";var eb=(new Date("2024-01-01T00:00:00Z")).getTime();function fb(a){var b=a.url;a=a.Xh;this.i=b;this.D=a;a=/[?&]dsh=1(&|$)/.test(b);this.u=!a&&/[?&]ae=1(&|$)/.test(b);this.M=!a&&/[?&]ae=2(&|$)/.test(b);if((this.h=/[?&]adurl=([^&]*)/.exec(b))&&this.h[1]){try{var c=decodeURIComponent(this.h[1])}catch(d){c=null}this.j=c}this.o=(new Date).getTime()-eb}
function gb(a,b){return b?a.h?a.i.slice(0,a.h.index)+b+a.i.slice(a.h.index):a.i+b:a.i}
function hb(a){a=a.D;if(!a)return"";var b="";a.platform&&(b+="&uap="+encodeURIComponent(a.platform));a.platformVersion&&(b+="&uapv="+encodeURIComponent(a.platformVersion));a.uaFullVersion&&(b+="&uafv="+encodeURIComponent(a.uaFullVersion));a.architecture&&(b+="&uaa="+encodeURIComponent(a.architecture));a.model&&(b+="&uam="+encodeURIComponent(a.model));a.bitness&&(b+="&uab="+encodeURIComponent(a.bitness));a.fullVersionList&&(b+="&uafvl="+encodeURIComponent(a.fullVersionList.map(function(c){return encodeURIComponent(c.brand)+
";"+encodeURIComponent(c.version)}).join("|")));
typeof a.wow64!=="undefined"&&(b+="&uaw="+Number(a.wow64));return b}
;var ib=String.prototype.trim?function(a){return a.trim()}:function(a){return/^[\s\xa0]*([\s\S]*?)[\s\xa0]*$/.exec(a)[1]};/*

 Copyright Google LLC
 SPDX-License-Identifier: Apache-2.0
*/
var jb=globalThis.trustedTypes,kb;function lb(){var a=null;if(!jb)return a;try{var b=function(c){return c};
a=jb.createPolicy("goog#html",{createHTML:b,createScript:b,createScriptURL:b})}catch(c){}return a}
function mb(){kb===void 0&&(kb=lb());return kb}
;function nb(a){this.h=a}
nb.prototype.toString=function(){return this.h+""};
function ob(a){var b=mb();return new nb(b?b.createScriptURL(a):a)}
function pb(a){if(a instanceof nb)return a.h;throw Error("");}
;var qb=sa([""]),rb=ta(["\x00"],["\\0"]),sb=ta(["\n"],["\\n"]),tb=ta(["\x00"],["\\u0000"]);function ub(a){return a.toString().indexOf("`")===-1}
ub(function(a){return a(qb)})||ub(function(a){return a(rb)})||ub(function(a){return a(sb)})||ub(function(a){return a(tb)});function vb(a){this.h=a}
vb.prototype.toString=function(){return this.h};
var wb=new vb("about:invalid#zClosurez");function xb(a){this.He=a}
function yb(a){return new xb(function(b){return b.substr(0,a.length+1).toLowerCase()===a+":"})}
var zb=[yb("data"),yb("http"),yb("https"),yb("mailto"),yb("ftp"),new xb(function(a){return/^[^:]*([/?#]|$)/.test(a)})],Ab=/^\s*(?!javascript:)(?:[\w+.-]+:|[^:/?#]*(?:[/?#]|$))/i;
function Bb(a){if(a instanceof vb)if(a instanceof vb)a=a.h;else throw Error("");else a=Ab.test(a)?a:void 0;return a}
;function Cb(a,b){b=Bb(b);b!==void 0&&(a.href=b)}
;function Db(a,b){throw Error(b===void 0?"unexpected value "+a+"!":b);}
;function Eb(a){this.h=a}
Eb.prototype.toString=function(){return this.h+""};function Fb(a){a=a===void 0?document:a;var b,c;a=(c=(b=a).querySelector)==null?void 0:c.call(b,"script[nonce]");return a==null?"":a.nonce||a.getAttribute("nonce")||""}
;function Gb(a){this.h=a}
Gb.prototype.toString=function(){return this.h+""};
function Hb(a){var b=mb();return new Gb(b?b.createScript(a):a)}
function Ib(a){if(a instanceof Gb)return a.h;throw Error("");}
;function Jb(a){var b=Fb(a.ownerDocument);b&&a.setAttribute("nonce",b)}
function Kb(a,b){a.src=pb(b);Jb(a)}
;function Lb(){this.h=Mb[0].toLowerCase()}
Lb.prototype.toString=function(){return this.h};function Nb(a){var b="true".toString(),c=[new Lb];if(c.length===0)throw Error("");if(c.map(function(d){if(d instanceof Lb)d=d.h;else throw Error("");return d}).every(function(d){return"data-loaded".indexOf(d)!==0}))throw Error('Attribute "data-loaded" does not match any of the allowed prefixes.');
a.setAttribute("data-loaded",b)}
;var Pb="alternate author bookmark canonical cite help icon license modulepreload next prefetch dns-prefetch prerender preconnect preload prev search subresource".split(" ");function Qb(a,b){if(b instanceof nb)a.href=pb(b).toString(),a.rel="stylesheet";else{if(Pb.indexOf("stylesheet")===-1)throw Error('TrustedResourceUrl href attribute required with rel="stylesheet"');b=Bb(b);b!==void 0&&(a.href=b,a.rel="stylesheet")}}
;var Rb=Array.prototype.indexOf?function(a,b){return Array.prototype.indexOf.call(a,b,void 0)}:function(a,b){if(typeof a==="string")return typeof b!=="string"||b.length!=1?-1:a.indexOf(b,0);
for(var c=0;c<a.length;c++)if(c in a&&a[c]===b)return c;return-1},Sb=Array.prototype.forEach?function(a,b){Array.prototype.forEach.call(a,b,void 0)}:function(a,b){for(var c=a.length,d=typeof a==="string"?a.split(""):a,e=0;e<c;e++)e in d&&b.call(void 0,d[e],e,a)},Tb=Array.prototype.filter?function(a,b){return Array.prototype.filter.call(a,b,void 0)}:function(a,b){for(var c=a.length,d=[],e=0,f=typeof a==="string"?a.split(""):a,g=0;g<c;g++)if(g in f){var h=f[g];
b.call(void 0,h,g,a)&&(d[e++]=h)}return d},Ub=Array.prototype.map?function(a,b){return Array.prototype.map.call(a,b,void 0)}:function(a,b){for(var c=a.length,d=Array(c),e=typeof a==="string"?a.split(""):a,f=0;f<c;f++)f in e&&(d[f]=b.call(void 0,e[f],f,a));
return d},Vb=Array.prototype.reduce?function(a,b,c){return Array.prototype.reduce.call(a,b,c)}:function(a,b,c){var d=c;
Sb(a,function(e,f){d=b.call(void 0,d,e,f,a)});
return d};
function Wb(a,b){a:{for(var c=a.length,d=typeof a==="string"?a.split(""):a,e=0;e<c;e++)if(e in d&&b.call(void 0,d[e],e,a)){b=e;break a}b=-1}return b<0?null:typeof a==="string"?a.charAt(b):a[b]}
function Xb(a,b){b=Rb(a,b);var c;(c=b>=0)&&Array.prototype.splice.call(a,b,1);return c}
function Yb(a,b){for(var c=1;c<arguments.length;c++){var d=arguments[c];if(Na(d)){var e=a.length||0,f=d.length||0;a.length=e+f;for(var g=0;g<f;g++)a[e+g]=d[g]}else a.push(d)}}
;function Zb(a,b){a.__closure__error__context__984382||(a.__closure__error__context__984382={});a.__closure__error__context__984382.severity=b}
;function $b(a){var b=E("window.location.href");a==null&&(a='Unknown Error of type "null/undefined"');if(typeof a==="string")return{message:a,name:"Unknown error",lineNumber:"Not available",fileName:b,stack:"Not available"};var c=!1;try{var d=a.lineNumber||a.line||"Not available"}catch(g){d="Not available",c=!0}try{var e=a.fileName||a.filename||a.sourceURL||C.$googDebugFname||b}catch(g){e="Not available",c=!0}b=ac(a);if(!(!c&&a.lineNumber&&a.fileName&&a.stack&&a.message&&a.name)){c=a.message;if(c==
null){if(a.constructor&&a.constructor instanceof Function){if(a.constructor.name)c=a.constructor.name;else if(c=a.constructor,bc[c])c=bc[c];else{c=String(c);if(!bc[c]){var f=/function\s+([^\(]+)/m.exec(c);bc[c]=f?f[1]:"[Anonymous]"}c=bc[c]}c='Unknown Error of type "'+c+'"'}else c="Unknown Error of unknown type";typeof a.toString==="function"&&Object.prototype.toString!==a.toString&&(c+=": "+a.toString())}return{message:c,name:a.name||"UnknownError",lineNumber:d,fileName:e,stack:b||"Not available"}}return{message:a.message,
name:a.name,lineNumber:a.lineNumber,fileName:a.fileName,stack:b}}
function ac(a,b){b||(b={});b[cc(a)]=!0;var c=a.stack||"",d=a.cause;d&&!b[cc(d)]&&(c+="\nCaused by: ",d.stack&&d.stack.indexOf(d.toString())==0||(c+=typeof d==="string"?d:d.message+"\n"),c+=ac(d,b));a=a.errors;if(Array.isArray(a)){d=1;var e;for(e=0;e<a.length&&!(d>4);e++)b[cc(a[e])]||(c+="\nInner error "+d++ +": ",a[e].stack&&a[e].stack.indexOf(a[e].toString())==0||(c+=typeof a[e]==="string"?a[e]:a[e].message+"\n"),c+=ac(a[e],b));e<a.length&&(c+="\n... "+(a.length-e)+" more inner errors")}return c}
function cc(a){var b="";typeof a.toString==="function"&&(b=""+a);return b+a.stack}
var bc={};function dc(a){for(var b=0,c=0;c<a.length;++c)b=31*b+a.charCodeAt(c)>>>0;return b}
;var ec=RegExp("^(?:([^:/?#.]+):)?(?://(?:([^\\\\/?#]*)@)?([^\\\\/?#]*?)(?::([0-9]+))?(?=[\\\\/?#]|$))?([^?#]+)?(?:\\?([^#]*))?(?:#([\\s\\S]*))?$");function fc(a){return a?decodeURI(a):a}
function hc(a,b){return b.match(ec)[a]||null}
function ic(a){return fc(hc(3,a))}
function jc(a){var b=a.match(ec);a=b[5];var c=b[6];b=b[7];var d="";a&&(d+=a);c&&(d+="?"+c);b&&(d+="#"+b);return d}
function kc(a){var b=a.indexOf("#");return b<0?a:a.slice(0,b)}
function lc(a,b,c){if(Array.isArray(b))for(var d=0;d<b.length;d++)lc(a,String(b[d]),c);else b!=null&&c.push(a+(b===""?"":"="+encodeURIComponent(String(b))))}
function mc(a){var b=[],c;for(c in a)lc(c,a[c],b);return b.join("&")}
function nc(a,b){b=mc(b);if(b){var c=a.indexOf("#");c<0&&(c=a.length);var d=a.indexOf("?");if(d<0||d>c){d=c;var e=""}else e=a.substring(d+1,c);a=[a.slice(0,d),e,a.slice(c)];c=a[1];a[1]=b?c?c+"&"+b:b:c;b=a[0]+(a[1]?"?"+a[1]:"")+a[2]}else b=a;return b}
function oc(a,b,c,d){for(var e=c.length;(b=a.indexOf(c,b))>=0&&b<d;){var f=a.charCodeAt(b-1);if(f==38||f==63)if(f=a.charCodeAt(b+e),!f||f==61||f==38||f==35)return b;b+=e+1}return-1}
var pc=/#|$/,qc=/[?&]($|#)/;function rc(a,b){for(var c=a.search(pc),d=0,e,f=[];(e=oc(a,d,b,c))>=0;)f.push(a.substring(d,e)),d=Math.min(a.indexOf("&",e)+1||c,c);f.push(a.slice(d));return f.join("").replace(qc,"$1")}
;function sc(){try{var a,b;return!!((a=window)==null?0:(b=a.top)==null?0:b.location.href)&&!1}catch(c){return!0}}
;function tc(a){var b=b===void 0?42:b;var c=[];uc(a,vc,6).forEach(function(d){wc(d,2)<=b&&c.push(wc(d,1))});
return c}
function xc(a){var b=b===void 0?42:b;var c=[];uc(a,vc,6).forEach(function(d){wc(d,2)>b&&c.push(wc(d,1))});
return c}
function yc(a){var b=b===void 0?42:b;a=(a==null?void 0:wc(a,1))||0;return a>0&&b>=a}
;function zc(a){a&&typeof a.dispose=="function"&&a.dispose()}
;function Ac(a){for(var b=0,c=arguments.length;b<c;++b){var d=arguments[b];Na(d)?Ac.apply(null,d):zc(d)}}
;function F(){this.ea=this.ea;this.M=this.M}
F.prototype.ea=!1;F.prototype.dispose=function(){this.ea||(this.ea=!0,this.ba())};
F.prototype[Symbol.dispose]=function(){this.dispose()};
function Bc(a,b){a.addOnDisposeCallback($a(zc,b))}
F.prototype.addOnDisposeCallback=function(a,b){this.ea?b!==void 0?a.call(b):a():(this.M||(this.M=[]),b&&(a=a.bind(b)),this.M.push(a))};
F.prototype.ba=function(){if(this.M)for(;this.M.length;)this.M.shift()()};var Cc;function Dc(){F.apply(this,arguments);this.j=1;this[Cc]=this.dispose}
w(Dc,F);Dc.prototype.share=function(){if(this.ea)throw Error("E:AD");this.j++;return this};
Dc.prototype.dispose=function(){--this.j||F.prototype.dispose.call(this)};
Cc=Symbol.dispose;function Ec(a){return{fieldType:2,fieldName:a}}
function Fc(a){return{fieldType:3,fieldName:a}}
;function Gc(a){this.h=a;a.Gc("/client_streamz/bg/frs",Fc("ke"))}
Gc.prototype.record=function(a,b){this.h.record("/client_streamz/bg/frs",a,b)};
function Hc(a){this.h=a;a.Gc("/client_streamz/bg/wrl",Fc("mn"),Ec("ac"),Ec("sc"),Fc("rk"),Fc("mk"))}
Hc.prototype.record=function(a,b,c,d,e,f){this.h.record("/client_streamz/bg/wrl",a,b,c,d,e,f)};
function Ic(a){this.i=a;a.Kb("/client_streamz/bg/ec",Fc("en"),Fc("mk"))}
Ic.prototype.h=function(a,b){this.i.Ib("/client_streamz/bg/ec",a,b)};
function Jc(a){this.h=a;a.Gc("/client_streamz/bg/el",Fc("en"),Fc("rk"),Fc("mk"))}
Jc.prototype.record=function(a,b,c,d){this.h.record("/client_streamz/bg/el",a,b,c,d)};
function Kc(a){this.i=a;a.Kb("/client_streamz/bg/cec",Ec("ec"),Fc("rk"),Fc("mk"))}
Kc.prototype.h=function(a,b,c){this.i.Ib("/client_streamz/bg/cec",a,b,c)};
function Lc(a){this.i=a;a.Kb("/client_streamz/bg/po/csc",Ec("cs"),Fc("rk"),Fc("mk"))}
Lc.prototype.h=function(a,b,c){this.i.Ib("/client_streamz/bg/po/csc",a,b,c)};
function Mc(a){this.i=a;a.Kb("/client_streamz/bg/po/ctav",Fc("av"),Fc("rk"),Fc("mk"))}
Mc.prototype.h=function(a,b,c){this.i.Ib("/client_streamz/bg/po/ctav",a,b,c)};
function Nc(a){this.i=a;a.Kb("/client_streamz/bg/po/cwsc",Fc("su"),Fc("rk"),Fc("mk"))}
Nc.prototype.h=function(a,b,c){this.i.Ib("/client_streamz/bg/po/cwsc",a,b,c)};function Oc(a){C.setTimeout(function(){throw a;},0)}
;var Pc,Qc=E("CLOSURE_FLAGS"),Rc=Qc&&Qc[610401301];Pc=Rc!=null?Rc:!1;function Sc(){var a=C.navigator;return a&&(a=a.userAgent)?a:""}
var Tc,Uc=C.navigator;Tc=Uc?Uc.userAgentData||null:null;function Vc(a){return Pc?Tc?Tc.brands.some(function(b){return(b=b.brand)&&b.indexOf(a)!=-1}):!1:!1}
function G(a){return Sc().indexOf(a)!=-1}
;function Wc(){return Pc?!!Tc&&Tc.brands.length>0:!1}
function Xc(){return Wc()?!1:G("Opera")}
function Yc(){return G("Firefox")||G("FxiOS")}
function Zc(){return Wc()?Vc("Chromium"):(G("Chrome")||G("CriOS"))&&!(Wc()?0:G("Edge"))||G("Silk")}
;function $c(){return Pc?!!Tc&&!!Tc.platform:!1}
function ad(){return G("iPhone")&&!G("iPod")&&!G("iPad")}
;var bd=Xc(),cd=Wc()?!1:G("Trident")||G("MSIE"),dd=G("Edge"),ed=G("Gecko")&&!(Sc().toLowerCase().indexOf("webkit")!=-1&&!G("Edge"))&&!(G("Trident")||G("MSIE"))&&!G("Edge"),fd=Sc().toLowerCase().indexOf("webkit")!=-1&&!G("Edge");fd&&G("Mobile");$c()||G("Macintosh");$c()||G("Windows");($c()?Tc.platform==="Linux":G("Linux"))||$c()||G("CrOS");var gd=$c()?Tc.platform==="Android":G("Android");ad();G("iPad");G("iPod");ad()||G("iPad")||G("iPod");Sc().toLowerCase().indexOf("kaios");Yc();var hd=ad()||G("iPod"),id=G("iPad");!G("Android")||Zc()||Yc()||Xc()||G("Silk");Zc();var jd=G("Safari")&&!(Zc()||(Wc()?0:G("Coast"))||Xc()||(Wc()?0:G("Edge"))||(Wc()?Vc("Microsoft Edge"):G("Edg/"))||(Wc()?Vc("Opera"):G("OPR"))||Yc()||G("Silk")||G("Android"))&&!(ad()||G("iPad")||G("iPod"));var kd={},ld=null;function md(a,b){Na(a);b===void 0&&(b=0);nd();b=kd[b];for(var c=Array(Math.floor(a.length/3)),d=b[64]||"",e=0,f=0;e<a.length-2;e+=3){var g=a[e],h=a[e+1],k=a[e+2],l=b[g>>2];g=b[(g&3)<<4|h>>4];h=b[(h&15)<<2|k>>6];k=b[k&63];c[f++]=""+l+g+h+k}l=0;k=d;switch(a.length-e){case 2:l=a[e+1],k=b[(l&15)<<2]||d;case 1:a=a[e],c[f]=""+b[a>>2]+b[(a&3)<<4|l>>4]+k+d}return c.join("")}
function od(a){var b=a.length,c=b*3/4;c%3?c=Math.floor(c):"=.".indexOf(a[b-1])!=-1&&(c="=.".indexOf(a[b-2])!=-1?c-2:c-1);var d=new Uint8Array(c),e=0;pd(a,function(f){d[e++]=f});
return e!==c?d.subarray(0,e):d}
function pd(a,b){function c(k){for(;d<a.length;){var l=a.charAt(d++),m=ld[l];if(m!=null)return m;if(!/^[\s\xa0]*$/.test(l))throw Error("Unknown base64 encoding at char: "+l);}return k}
nd();for(var d=0;;){var e=c(-1),f=c(0),g=c(64),h=c(64);if(h===64&&e===-1)break;b(e<<2|f>>4);g!=64&&(b(f<<4&240|g>>2),h!=64&&b(g<<6&192|h))}}
function nd(){if(!ld){ld={};for(var a="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".split(""),b=["+/=","+/","-_=","-_.","-_"],c=0;c<5;c++){var d=a.concat(b[c].split(""));kd[c]=d;for(var e=0;e<d.length;e++){var f=d[e];ld[f]===void 0&&(ld[f]=e)}}}}
;var qd=typeof Uint8Array!=="undefined",rd=!cd&&typeof btoa==="function";function sd(a){if(!rd)return md(a);for(var b="",c=0,d=a.length-10240;c<d;)b+=String.fromCharCode.apply(null,a.subarray(c,c+=10240));b+=String.fromCharCode.apply(null,c?a.subarray(c):a);return btoa(b)}
var td=/[-_.]/g,ud={"-":"+",_:"/",".":"="};function vd(a){return ud[a]||""}
function wd(a){return qd&&a!=null&&a instanceof Uint8Array}
var xd={};function yd(a,b){zd(b);this.h=a;if(a!=null&&a.length===0)throw Error("ByteString should be constructed with non-empty values");}
yd.prototype.sizeBytes=function(){zd(xd);var a=this.h;if(a!=null&&!wd(a))if(typeof a==="string")if(rd){td.test(a)&&(a=a.replace(td,vd));a=atob(a);for(var b=new Uint8Array(a.length),c=0;c<a.length;c++)b[c]=a.charCodeAt(c);a=b}else a=od(a);else Ma(a),a=null;return(a=a==null?a:this.h=a)?a.length:0};
var Ad;function zd(a){if(a!==xd)throw Error("illegal external caller");}
;var Bd=void 0;function Cd(a){a=Error(a);Zb(a,"warning");return a}
;var Dd=typeof Symbol==="function"&&typeof Symbol()==="symbol",Ed=new Set;function Fd(a,b,c,d){c=c===void 0?!1:c;a=typeof Symbol==="function"&&typeof Symbol()==="symbol"?(d===void 0?0:d)&&Symbol.for&&a?Symbol.for(a):a!=null?Symbol(a):Symbol():b;c&&Ed.add(a);return a}
var Gd=Fd("jas",void 0,!0,!0),Hd=Fd(void 0,"2ex"),Id=Fd(void 0,"1oa",!0),Jd=Fd(void 0,Symbol(),!0);Math.max.apply(Math,ra(Object.values({mh:1,kh:2,jh:4,ph:8,oh:16,nh:32,Of:64,rh:128,ih:256,hh:512,lh:1024,Uf:2048,qh:4096,Vf:8192,Pf:16384})));var I=Dd?Gd:"Ge",Kd={Ge:{value:0,configurable:!0,writable:!0,enumerable:!1}},Ld=Object.defineProperties;function Md(a,b){Dd||I in a||Ld(a,Kd);a[I]|=b}
function Nd(a,b){Dd||I in a||Ld(a,Kd);a[I]=b}
function Od(a,b){Nd(b,(a|0)&-30975)}
function Pd(a,b){Nd(b,(a|34)&-30941)}
;function Qd(){return typeof BigInt==="function"}
;function Rd(a){return Array.prototype.slice.call(a)}
;var Sd={},Td={};function Ud(a){return!(!a||typeof a!=="object"||a.h!==Td)}
function Vd(a){return a!==null&&typeof a==="object"&&!Array.isArray(a)&&a.constructor===Object}
function Wd(a){return!Array.isArray(a)||a.length?!1:(a[I]|0)&1?!0:!1}
var Xd,Yd=[];Nd(Yd,55);Xd=Object.freeze(Yd);function Zd(a){if(a&2)throw Error();}
function $d(a,b){var c=bb(Jd);(b=c?b[c]:void 0)&&(a[Jd]=Rd(b))}
var ae=Object.freeze({});function be(a){a.Fh=!0;return a}
;var ce=be(function(a){return typeof a==="number"}),de=be(function(a){return typeof a==="string"}),ee=be(function(a){return typeof a==="boolean"});
function fe(){var a=ge;return be(function(b){for(var c in a)if(b===a[c]&&!/^[0-9]+$/.test(c))return!0;return!1})}
var he=be(function(a){return a!=null&&typeof a==="object"&&typeof a.then==="function"});var ie=typeof C.BigInt==="function"&&typeof C.BigInt(0)==="bigint";function je(a){var b=a;if(de(b)){if(!/^\s*(?:-?[1-9]\d*|0)?\s*$/.test(b))throw Error(String(b));}else if(ce(b)&&!Number.isSafeInteger(b))throw Error(String(b));return ie?BigInt(a):a=ee(a)?a?"1":"0":de(a)?a.trim()||"0":String(a)}
var pe=be(function(a){return ie?a>=ke&&a<=le:a[0]==="-"?me(a,ne):me(a,oe)}),ne=Number.MIN_SAFE_INTEGER.toString(),ke=ie?BigInt(Number.MIN_SAFE_INTEGER):void 0,oe=Number.MAX_SAFE_INTEGER.toString(),le=ie?BigInt(Number.MAX_SAFE_INTEGER):void 0;
function me(a,b){if(a.length>b.length)return!1;if(a.length<b.length||a===b)return!0;for(var c=0;c<a.length;c++){var d=a[c],e=b[c];if(d>e)return!1;if(d<e)return!0}}
;var qe=0,re=0;function se(a){var b=a>>>0;qe=b;re=(a-b)/4294967296>>>0}
function te(a){if(a<0){se(0-a);var b=y(ue(qe,re));a=b.next().value;b=b.next().value;qe=a>>>0;re=b>>>0}else se(a)}
function ve(a,b){b>>>=0;a>>>=0;if(b<=2097151)var c=""+(4294967296*b+a);else Qd()?c=""+(BigInt(b)<<BigInt(32)|BigInt(a)):(c=(a>>>24|b<<8)&16777215,b=b>>16&65535,a=(a&16777215)+c*6777216+b*6710656,c+=b*8147497,b*=2,a>=1E7&&(c+=a/1E7>>>0,a%=1E7),c>=1E7&&(b+=c/1E7>>>0,c%=1E7),c=b+we(c)+we(a));return c}
function we(a){a=String(a);return"0000000".slice(a.length)+a}
function xe(){var a=qe,b=re;b&2147483648?Qd()?a=""+(BigInt(b|0)<<BigInt(32)|BigInt(a>>>0)):(b=y(ue(a,b)),a=b.next().value,b=b.next().value,a="-"+ve(a,b)):a=ve(a,b);return a}
function ue(a,b){b=~b;a?a=~a+1:b+=1;return[a,b]}
;var ye=typeof BigInt==="function"?BigInt.asIntN:void 0,ze=Number.isSafeInteger,Ae=Number.isFinite,Be=Math.trunc;function Ce(a){return a.displayName||a.name||"unknown type name"}
function De(a){if(a!=null&&typeof a!=="boolean")throw Error("Expected boolean but got "+Ma(a)+": "+a);return a}
var Ee=/^-?([1-9][0-9]*|0)(\.[0-9]+)?$/;function Fe(a){switch(typeof a){case "bigint":return!0;case "number":return Ae(a);case "string":return Ee.test(a);default:return!1}}
function Ge(a){if(typeof a!=="number")throw Cd("int32");if(!Ae(a))throw Cd("int32");return a|0}
function He(a){return a==null?a:Ge(a)}
function Ie(a){if(a==null)return a;if(typeof a==="string"&&a)a=+a;else if(typeof a!=="number")return;return Ae(a)?a|0:void 0}
function Je(a){var b=0;b=b===void 0?0:b;if(!Fe(a))throw Cd("int64");var c=typeof a;switch(b){case 4096:switch(c){case "string":return Ke(a);case "bigint":return String(ye(64,a));default:return Le(a)}case 8192:switch(c){case "string":return b=Be(Number(a)),ze(b)?a=je(b):(b=a.indexOf("."),b!==-1&&(a=a.substring(0,b)),a=Qd()?je(ye(64,BigInt(a))):je(Me(a))),a;case "bigint":return je(ye(64,a));default:return ze(a)?je(Ne(a)):je(Le(a))}case 0:switch(c){case "string":return Ke(a);case "bigint":return je(ye(64,
a));default:return Ne(a)}default:return Db(b,"Unknown format requested type for int64")}}
function Oe(a){return a==null?a:Je(a)}
function Pe(a){var b=a.length;return a[0]==="-"?b<20?!0:b===20&&Number(a.substring(0,7))>-922337:b<19?!0:b===19&&Number(a.substring(0,6))<922337}
function Me(a){a.indexOf(".");if(Pe(a))return a;if(a.length<16)te(Number(a));else if(Qd())a=BigInt(a),qe=Number(a&BigInt(4294967295))>>>0,re=Number(a>>BigInt(32)&BigInt(4294967295));else{var b=+(a[0]==="-");re=qe=0;for(var c=a.length,d=0+b,e=(c-b)%6+b;e<=c;d=e,e+=6)d=Number(a.slice(d,e)),re*=1E6,qe=qe*1E6+d,qe>=4294967296&&(re+=Math.trunc(qe/4294967296),re>>>=0,qe>>>=0);b&&(b=y(ue(qe,re)),a=b.next().value,b=b.next().value,qe=a,re=b)}return xe()}
function Ne(a){Fe(a);a=Be(a);if(!ze(a)){te(a);var b=qe,c=re;if(a=c&2147483648)b=~b+1>>>0,c=~c>>>0,b==0&&(c=c+1>>>0);var d=c*4294967296+(b>>>0);b=Number.isSafeInteger(d)?d:ve(b,c);a=typeof b==="number"?a?-b:b:a?"-"+b:b}return a}
function Le(a){Fe(a);a=Be(a);if(ze(a))a=String(a);else{var b=String(a);Pe(b)?a=b:(te(a),a=xe())}return a}
function Ke(a){Fe(a);var b=Be(Number(a));if(ze(b))return String(b);b=a.indexOf(".");b!==-1&&(a=a.substring(0,b));return Me(a)}
function Qe(a){if(a==null)return a;if(typeof a==="bigint")return pe(a)?a=Number(a):(a=ye(64,a),a=pe(a)?Number(a):String(a)),a;if(Fe(a))return typeof a==="number"?Ne(a):Ke(a)}
function Re(a){if(typeof a!=="string")throw Error();return a}
function Se(a){if(a!=null&&typeof a!=="string")throw Error();return a}
function Te(a,b){if(!(a instanceof b))throw Error("Expected instanceof "+Ce(b)+" but got "+(a&&Ce(a.constructor)));}
function Ue(a,b,c){if(a!=null&&typeof a==="object"&&a.Sc===Sd)return a;if(Array.isArray(a)){var d=a[I]|0,e=d;e===0&&(e|=c&32);e|=c&2;e!==d&&Nd(a,e);return new b(a)}}
;function Ve(a,b){return We(b)}
function We(a){switch(typeof a){case "number":return isFinite(a)?a:String(a);case "bigint":return pe(a)?Number(a):String(a);case "boolean":return a?1:0;case "object":if(a)if(Array.isArray(a)){if(Wd(a))return}else{if(wd(a))return sd(a);if(a instanceof yd){var b=a.h;return b==null?"":typeof b==="string"?b:a.h=sd(b)}}}return a}
;function Xe(a,b,c){var d=Rd(a),e=d.length,f=b&256?d[e-1]:void 0;e+=f?-1:0;for(b=b&512?1:0;b<e;b++)d[b]=c(d[b]);if(f){b=d[b]={};for(var g in f)b[g]=c(f[g])}$d(d,a);return d}
function Ye(a,b,c,d,e){if(a!=null){if(Array.isArray(a))a=Wd(a)?void 0:e&&(a[I]|0)&2?a:Ze(a,b,c,d!==void 0,e);else if(Vd(a)){var f={},g;for(g in a)f[g]=Ye(a[g],b,c,d,e);a=f}else a=b(a,d);return a}}
function Ze(a,b,c,d,e){var f=d||c?a[I]|0:0;d=d?!!(f&32):void 0;for(var g=Rd(a),h=0;h<g.length;h++)g[h]=Ye(g[h],b,c,d,e);c&&($d(g,a),c(f,g));return g}
function $e(a){return a.Sc===Sd?a.toJSON():We(a)}
function af(a){return Ze(a,$e,void 0,void 0,!1)}
;function J(a,b,c){if(a==null){var d=96;c?(a=[c],d|=512):a=[];b&&(d=d&-33521665|(b&1023)<<15)}else{if(!Array.isArray(a))throw Error("narr");d=a[I]|0;if(d&2048)throw Error("farr");if(d&64)return a;d|=64;if(c&&(d|=512,c!==a[0]))throw Error("mid");a:{c=a;var e=c.length;if(e){var f=e-1;if(Vd(c[f])){d|=256;b=f-(d&512?0:-1);if(b>=1024)throw Error("pvtlmt");d=d&-33521665|(b&1023)<<15;break a}}if(b){b=Math.max(b,e-(d&512?0:-1));if(b>1024)throw Error("spvt");d=d&-33521665|(b&1023)<<15}}}Nd(a,d);return a}
;function bf(a,b,c){c=c===void 0?Pd:c;if(a!=null){if(qd&&a instanceof Uint8Array)return b?a:new Uint8Array(a);if(Array.isArray(a)){var d=a[I]|0;if(d&2)return a;b&&(b=d===0||!!(d&32)&&!(d&64||!(d&16)));return b?(Nd(a,(d|34)&-12293),a):Ze(a,bf,d&4?Pd:c,!0,!0)}a.Sc===Sd&&(c=a.F,d=c[I]|0,a=d&2?a:new a.constructor(cf(c,d,!0)));return a}}
function cf(a,b,c){var d=c||b&2?Pd:Od,e=!!(b&32);a=Xe(a,b,function(f){return bf(f,e,d)});
Md(a,32|(c?2:0));return a}
function df(a){var b=a.F,c=b[I]|0;return c&2?new a.constructor(cf(b,c,!1)):a}
;function ef(a,b){a=a.F;return ff(a,a[I]|0,b)}
function ff(a,b,c,d){if(c===-1)return null;var e=c+(b&512?0:-1),f=a.length-1;if(e>=f&&b&256)return a[f][c];if(d&&b&256&&(b=a[f][c],b!=null)){if(a[e]!=null&&Hd!=null){var g;a=(g=Bd)!=null?g:Bd={};g=a[Hd]||0;g>=4||(a[Hd]=g+1,g=Error(),Zb(g,"incident"),Oc(g))}return b}if(e<=f)return a[e]}
function gf(a,b,c){var d=a.F,e=d[I]|0;Zd(e);hf(d,e,b,c);return a}
function hf(a,b,c,d){var e=b&512?0:-1,f=c+e,g=a.length-1;if(f>=g&&b&256)return a[g][c]=d,b;if(f<=g)return a[f]=d,b&256&&(a=a[g],c in a&&delete a[c]),b;d!==void 0&&(g=b>>15&1023||536870912,c>=g?d!=null&&(f={},a[g+e]=(f[c]=d,f),b|=256,Nd(a,b)):a[f]=d);return b}
function jf(a){return kf(a,lf,11,!1)!==void 0}
function mf(a){return!!(2&a)&&!!(4&a)||!!(2048&a)}
function nf(a,b,c){var d=a.F,e=d[I]|0;Zd(e);if(b==null)return hf(d,e,3),a;if(!Array.isArray(b))throw Cd();var f=b[I]|0,g=f,h=mf(f),k=h||Object.isFrozen(b);h||(f=0);k||(b=Rd(b),g=0,f=of(f,e),f=pf(f,e,!0),k=!1);f|=21;h=4&f?4096&f?4096:8192&f?8192:0:void 0;h=h!=null?h:0;for(var l=0;l<b.length;l++){var m=b[l],n=c(m,h);Object.is(m,n)||(k&&(b=Rd(b),g=0,f=of(f,e),f=pf(f,e,!0),k=!1),b[l]=n)}f!==g&&(k&&(b=Rd(b),f=of(f,e),f=pf(f,e,!0)),Nd(b,f));hf(d,e,3,b);return a}
function qf(a,b,c,d){a=a.F;var e=a[I]|0;Zd(e);if(d==null){var f=rf(a);if(sf(f,a,e,c)===b)f.set(c,0);else return}else{c.includes(b);f=rf(a);var g=sf(f,a,e,c);g!==b&&(g&&(e=hf(a,e,g)),f.set(c,b))}hf(a,e,b,d)}
function rf(a){if(Dd){var b;return(b=a[Id])!=null?b:a[Id]=new Map}if(Id in a)return a[Id];b=new Map;Object.defineProperty(a,Id,{value:b});return b}
function sf(a,b,c,d){var e=a.get(d);if(e!=null)return e;for(var f=e=0;f<d.length;f++){var g=d[f];ff(b,c,g)!=null&&(e!==0&&(c=hf(b,c,e)),e=g)}a.set(d,e);return e}
function kf(a,b,c,d){a=a.F;var e=a[I]|0;d=ff(a,e,c,d);b=Ue(d,b,e);b!==d&&b!=null&&hf(a,e,c,b);return b}
function tf(a,b,c,d){b=kf(a,b,c,d===void 0?!1:d);if(b==null)return b;a=a.F;d=a[I]|0;if(!(d&2)){var e=df(b);e!==b&&(b=e,hf(a,d,c,b))}return b}
function uc(a,b,c){var d=void 0===ae?2:4;var e=a.F[I]|0,f=e,g=!(2&e);a=a.F;var h=!!(2&f);e=h?1:d;g&&(g=!h);d=ff(a,f,c);d=Array.isArray(d)?d:Xd;var k=d[I]|0;h=!!(4&k);if(!h){var l=k;l===0&&(l=of(l,f));k=d;l|=1;var m=f,n=!!(2&l);n&&(m|=2);for(var p=!n,t=!0,u=0,x=0;u<k.length;u++){var z=Ue(k[u],b,m);if(z instanceof b){if(!n){var H=!!((z.F[I]|0)&2);p&&(p=!H);t&&(t=H)}k[x++]=z}}x<u&&(k.length=x);l|=4;l=t?l|16:l&-17;l=p?l|8:l&-9;Nd(k,l);n&&Object.freeze(k);k=l}if(g&&!(8&k||!d.length&&(e===1||e===4&&32&
k))){mf(k)&&(d=Rd(d),k=of(k,f),f=hf(a,f,c,d));b=d;g=k;for(k=0;k<b.length;k++)l=b[k],m=df(l),l!==m&&(b[k]=m);g|=8;g=b.length?g&-17:g|16;Nd(b,g);k=g}e===1||e===4&&32&k?mf(k)||(f=k,c=!!(32&k),k|=!d.length||16&k&&(!h||c)?2:2048,k!==f&&Nd(d,k),Object.freeze(d)):(e===2&&mf(k)&&(d=Rd(d),k=of(k,f),k=pf(k,f,!1),Nd(d,k),f=hf(a,f,c,d)),mf(k)||(c=k,k=pf(k,f,!1),k!==c&&Nd(d,k)));return d}
function uf(a,b,c,d){d!=null?Te(d,b):d=void 0;return gf(a,c,d)}
function vf(a,b,c,d){var e=a.F,f=e[I]|0;Zd(f);if(d==null)return hf(e,f,c),a;if(!Array.isArray(d))throw Cd();for(var g=d[I]|0,h=g,k=mf(g),l=k||Object.isFrozen(d),m=!0,n=!0,p=0;p<d.length;p++){var t=d[p];Te(t,b);k||(t=!!((t.F[I]|0)&2),m&&(m=!t),n&&(n=t))}k||(g=m?13:5,g=n?g|16:g&-17);l&&g===h||(d=Rd(d),h=0,g=of(g,f),g=pf(g,f,!0));g!==h&&Nd(d,g);hf(e,f,c,d);return a}
function of(a,b){a=(2&b?a|2:a&-3)|32;return a&=-2049}
function pf(a,b,c){32&b&&c||(a&=-33);return a}
function wf(a){a=ef(a,1);var b=b===void 0?!1:b;var c=typeof a;b=a==null?a:c==="bigint"?String(ye(64,a)):Fe(a)?c==="string"?Ke(a):b?Le(a):Ne(a):void 0;return b}
function xf(a,b){a=ef(a,b);return a==null||typeof a==="string"?a:void 0}
function yf(a){a=ef(a,1);return a==null?a:Ae(a)?a|0:void 0}
function wc(a,b,c){c=c===void 0?0:c;var d;return(d=Ie(ef(a,b)))!=null?d:c}
function zf(a,b){var c=c===void 0?"":c;var d;return(d=xf(a,b))!=null?d:c}
function Af(a){var b=0;b=b===void 0?0:b;var c;return(c=yf(a))!=null?c:b}
function Bf(a,b,c){return gf(a,b,Se(c))}
function Cf(a,b,c){c=Se(c);a=a.F;var d=a[I]|0;Zd(d);hf(a,d,b,c===""?void 0:c)}
function Df(a,b,c){if(c!=null){if(!Ae(c))throw Cd("enum");c|=0}return gf(a,b,c)}
;function Ef(a){return a}
function Ff(a){return a}
function Gf(a,b,c,d){return Hf(a,b,c,d,If,Jf)}
function Kf(a,b,c,d){return Hf(a,b,c,d,Lf,Mf)}
function Hf(a,b,c,d,e,f){if(!c.length&&!d)return 0;for(var g=0,h=0,k=0,l=0,m=0,n=c.length-1;n>=0;n--){var p=c[n];d&&n===c.length-1&&p===d||(l++,p!=null&&k++)}if(d)for(var t in d)n=+t,isNaN(n)||(m+=Nf(n),h++,n>g&&(g=n));l=e(l,k)+f(h,g,m);t=k;n=h;p=g;for(var u=m,x=c.length-1;x>=0;x--){var z=c[x];if(!(z==null||d&&x===c.length-1&&z===d)){z=x-b;var H=e(z,t)+f(n,p,u);H<l&&(a=1+z,l=H);n++;t--;u+=Nf(z);p=Math.max(p,z)}}b=e(0,0)+f(n,p,u);b<l&&(a=0,l=b);if(d){n=h;p=g;u=m;t=k;for(var K in d)d=+K,isNaN(d)||d>=
1024||(n--,t++,u-=K.length,g=e(d,t)+f(n,p,u),g<l&&(a=1+d,l=g))}return a}
function Mf(a,b,c){return c+a*3+(a>1?a-1:0)}
function Lf(a,b){return(a>1?a-1:0)+(a-b)*4}
function Jf(a,b){return a==0?0:9*Math.max(1<<32-Math.clz32(a+a/2-1),4)<=b?a==0?0:a<4?100+(a-1)*16:a<6?148+(a-4)*16:a<12?244+(a-6)*16:a<22?436+(a-12)*19:a<44?820+(a-22)*17:52+32*a:40+4*b}
function If(a){return 40+4*a}
function Nf(a){return a>=100?a>=1E4?Math.ceil(Math.log10(1+a)):a<1E3?3:4:a<10?1:2}
;var Of;function Pf(a){return a}
var Qf;function L(a,b,c){this.F=J(a,b,c)}
r=L.prototype;r.toJSON=function(){var a=!Qf;try{return a&&(Qf=af),Rf(this)}finally{a&&(Qf=void 0)}};
r.serialize=function(a){try{return Qf=Pf,a&&(Of=a===Ff||a!==Ef&&a!==Gf&&a!==Kf?Ff:a),JSON.stringify(Rf(this),Ve)}finally{a&&(Of=void 0),Qf=void 0}};
function Sf(a,b){if(b==null||b=="")return new a;b=JSON.parse(b);if(!Array.isArray(b))throw Error("dnarr");Md(b,32);return new a(b)}
r.clone=function(){var a=this.F;return new this.constructor(cf(a,a[I]|0,!1))};
r.Sc=Sd;r.toString=function(){try{return Qf=Pf,Rf(this).toString()}finally{Qf=void 0}};
function Rf(a){var b=a.F,c=Qf(b);b=c!==b;var d=(b?a.F:c)[I]|0;if(a=c.length){var e=c[a-1],f=Vd(e);f?a--:e=void 0;var g=d&512?0:-1,h=a-g;d=!!Of&&!(d&512);var k,l=(k=Of)!=null?k:Ff;k=d?l(h,g,c,e):h;d=(h=d&&h!==k)?Array.prototype.slice.call(c,0,a):c;if(f||h){b:{var m=d;var n=e;var p;f=!1;if(h)for(l=Math.max(0,k+g);l<m.length;l++){var t=m[l],u=l-g;t==null||Wd(t)||Ud(t)&&t.size===0||(f=m[l]=void 0,((f=p)!=null?f:p={})[u]=t,f=!0)}if(n)for(var x in n)if(l=+x,isNaN(l))l=void 0,((l=p)!=null?l:p={})[x]=n[x];
else if(t=n[x],Array.isArray(t)&&(Wd(t)||Ud(t)&&t.size===0)&&(t=null),t==null&&(f=!0),h&&l<k){f=!0;t=l+g;for(u=m.length;u<=t;u++)m.push(void 0);m[t]=n[l]}else t!=null&&(l=void 0,((l=p)!=null?l:p={})[x]=t);f||(p=n);if(p)for(var z in p){n=p;break b}n=null}m=n==null?e!=null:n!==e}h&&(a=d.length);for(var H;a>0;a--){p=d[a-1];if(!(p==null||Wd(p)||Ud(p)&&p.size===0))break;H=!0}if(d!==c||m||H){if(!h&&!b)d=Array.prototype.slice.call(d,0,a);else if(H||m||n)d.length=a;n&&d.push(n)}c=d}return c}
;function Tf(a){return function(b){return Sf(a,b)}}
;function Uf(a){this.F=J(a)}
w(Uf,L);function Vf(a,b){return nf(a,b,Ge)}
;function Wf(a){this.F=J(a)}
w(Wf,L);var Xf=[1,2,3];function Yf(a){this.F=J(a)}
w(Yf,L);var Zf=[1,2,3];function $f(a){this.F=J(a)}
w($f,L);function ag(a){this.F=J(a)}
w(ag,L);function bg(a){this.F=J(a)}
w(bg,L);function cg(a){if(!a)return"";if(/^about:(?:blank|srcdoc)$/.test(a))return window.origin||"";a.indexOf("blob:")===0&&(a=a.substring(5));a=a.split("#")[0].split("?")[0];a=a.toLowerCase();a.indexOf("//")==0&&(a=window.location.protocol+a);/^[\w\-]*:\/\//.test(a)||(a=window.location.href);var b=a.substring(a.indexOf("://")+3),c=b.indexOf("/");c!=-1&&(b=b.substring(0,c));c=a.substring(0,a.indexOf("://"));if(!c)throw Error("URI is missing protocol: "+a);if(c!=="http"&&c!=="https"&&c!=="chrome-extension"&&
c!=="moz-extension"&&c!=="file"&&c!=="android-app"&&c!=="chrome-search"&&c!=="chrome-untrusted"&&c!=="chrome"&&c!=="app"&&c!=="devtools")throw Error("Invalid URI scheme in origin: "+c);a="";var d=b.indexOf(":");if(d!=-1){var e=b.substring(d+1);b=b.substring(0,d);if(c==="http"&&e!=="80"||c==="https"&&e!=="443")a=":"+e}return c+"://"+b+a}
;function dg(){function a(){e[0]=1732584193;e[1]=4023233417;e[2]=2562383102;e[3]=271733878;e[4]=3285377520;m=l=0}
function b(n){for(var p=g,t=0;t<64;t+=4)p[t/4]=n[t]<<24|n[t+1]<<16|n[t+2]<<8|n[t+3];for(t=16;t<80;t++)n=p[t-3]^p[t-8]^p[t-14]^p[t-16],p[t]=(n<<1|n>>>31)&4294967295;n=e[0];var u=e[1],x=e[2],z=e[3],H=e[4];for(t=0;t<80;t++){if(t<40)if(t<20){var K=z^u&(x^z);var da=1518500249}else K=u^x^z,da=1859775393;else t<60?(K=u&x|z&(u|x),da=2400959708):(K=u^x^z,da=3395469782);K=((n<<5|n>>>27)&4294967295)+K+H+da+p[t]&4294967295;H=z;z=x;x=(u<<30|u>>>2)&4294967295;u=n;n=K}e[0]=e[0]+n&4294967295;e[1]=e[1]+u&4294967295;
e[2]=e[2]+x&4294967295;e[3]=e[3]+z&4294967295;e[4]=e[4]+H&4294967295}
function c(n,p){if(typeof n==="string"){n=unescape(encodeURIComponent(n));for(var t=[],u=0,x=n.length;u<x;++u)t.push(n.charCodeAt(u));n=t}p||(p=n.length);t=0;if(l==0)for(;t+64<p;)b(n.slice(t,t+64)),t+=64,m+=64;for(;t<p;)if(f[l++]=n[t++],m++,l==64)for(l=0,b(f);t+64<p;)b(n.slice(t,t+64)),t+=64,m+=64}
function d(){var n=[],p=m*8;l<56?c(h,56-l):c(h,64-(l-56));for(var t=63;t>=56;t--)f[t]=p&255,p>>>=8;b(f);for(t=p=0;t<5;t++)for(var u=24;u>=0;u-=8)n[p++]=e[t]>>u&255;return n}
for(var e=[],f=[],g=[],h=[128],k=1;k<64;++k)h[k]=0;var l,m;a();return{reset:a,update:c,digest:d,ke:function(){for(var n=d(),p="",t=0;t<n.length;t++)p+="0123456789ABCDEF".charAt(Math.floor(n[t]/16))+"0123456789ABCDEF".charAt(n[t]%16);return p}}}
;function eg(a,b,c){var d=String(C.location.href);return d&&a&&b?[b,fg(cg(d),a,c||null)].join(" "):null}
function fg(a,b,c){var d=[],e=[];if((Array.isArray(c)?2:1)==1)return e=[b,a],Sb(d,function(h){e.push(h)}),gg(e.join(" "));
var f=[],g=[];Sb(c,function(h){g.push(h.key);f.push(h.value)});
c=Math.floor((new Date).getTime()/1E3);e=f.length==0?[c,b,a]:[f.join(":"),c,b,a];Sb(d,function(h){e.push(h)});
a=gg(e.join(" "));a=[c,a];g.length==0||a.push(g.join(""));return a.join("_")}
function gg(a){var b=dg();b.update(a);return b.ke().toLowerCase()}
;function hg(a){this.h=a||{cookie:""}}
r=hg.prototype;r.isEnabled=function(){if(!C.navigator.cookieEnabled)return!1;if(this.h.cookie)return!0;this.set("TESTCOOKIESENABLED","1",{Tb:60});if(this.get("TESTCOOKIESENABLED")!=="1")return!1;this.remove("TESTCOOKIESENABLED");return!0};
r.set=function(a,b,c){var d=!1;if(typeof c==="object"){var e=c.cf;d=c.secure||!1;var f=c.domain||void 0;var g=c.path||void 0;var h=c.Tb}if(/[;=\s]/.test(a))throw Error('Invalid cookie name "'+a+'"');if(/[;\r\n]/.test(b))throw Error('Invalid cookie value "'+b+'"');h===void 0&&(h=-1);c=f?";domain="+f:"";g=g?";path="+g:"";d=d?";secure":"";h=h<0?"":h==0?";expires="+(new Date(1970,1,1)).toUTCString():";expires="+(new Date(Date.now()+h*1E3)).toUTCString();this.h.cookie=a+"="+b+c+g+h+d+(e!=null?";samesite="+
e:"")};
r.get=function(a,b){for(var c=a+"=",d=(this.h.cookie||"").split(";"),e=0,f;e<d.length;e++){f=ib(d[e]);if(f.lastIndexOf(c,0)==0)return f.slice(c.length);if(f==a)return""}return b};
r.remove=function(a,b,c){var d=this.get(a)!==void 0;this.set(a,"",{Tb:0,path:b,domain:c});return d};
r.clear=function(){for(var a=(this.h.cookie||"").split(";"),b=[],c=[],d,e,f=0;f<a.length;f++)e=ib(a[f]),d=e.indexOf("="),d==-1?(b.push(""),c.push(e)):(b.push(e.substring(0,d)),c.push(e.substring(d+1)));for(a=b.length-1;a>=0;a--)this.remove(b[a])};
var ig=new hg(typeof document=="undefined"?null:document);function jg(){var a=C.__SAPISID||C.__APISID||C.__3PSAPISID||C.__1PSAPISID||C.__OVERRIDE_SID;if(a)return!0;typeof document!=="undefined"&&(a=new hg(document),a=a.get("SAPISID")||a.get("APISID")||a.get("__Secure-3PAPISID")||a.get("__Secure-1PAPISID"));return!!a}
function kg(a,b,c,d){(a=C[a])||typeof document==="undefined"||(a=(new hg(document)).get(b));return a?eg(a,c,d):null}
function lg(a){var b=cg(String(C.location.href)),c=[];if(jg()){b=b.indexOf("https:")==0||b.indexOf("chrome-extension:")==0||b.indexOf("chrome-untrusted://new-tab-page")==0||b.indexOf("moz-extension:")==0;var d=b?C.__SAPISID:C.__APISID;d||typeof document==="undefined"||(d=new hg(document),d=d.get(b?"SAPISID":"APISID")||d.get("__Secure-3PAPISID"));(d=d?eg(d,b?"SAPISIDHASH":"APISIDHASH",a):null)&&c.push(d);b&&((b=kg("__1PSAPISID","__Secure-1PAPISID","SAPISID1PHASH",a))&&c.push(b),(a=kg("__3PSAPISID",
"__Secure-3PAPISID","SAPISID3PHASH",a))&&c.push(a))}return c.length==0?null:c.join(" ")}
;function mg(){}
mg.prototype.compress=function(a){var b,c,d,e;return A(function(f){switch(f.h){case 1:return b=new CompressionStream("gzip"),c=(new Response(b.readable)).arrayBuffer(),d=b.writable.getWriter(),f.yield(d.write((new TextEncoder).encode(a)),2);case 2:return f.yield(d.close(),3);case 3:return e=Uint8Array,f.yield(c,4);case 4:return f.return(new e(f.i))}})};
mg.prototype.isSupported=function(a){return a<1024?!1:typeof CompressionStream!=="undefined"};function ng(a){this.F=J(a)}
w(ng,L);function og(a,b){this.intervalMs=a;this.callback=b;this.enabled=!1;this.h=function(){return ab()};
this.i=this.h()}
og.prototype.setInterval=function(a){this.intervalMs=a;this.timer&&this.enabled?(this.stop(),this.start()):this.timer&&this.stop()};
og.prototype.start=function(){var a=this;this.enabled=!0;this.timer||(this.timer=setTimeout(function(){a.tick()},this.intervalMs),this.i=this.h())};
og.prototype.stop=function(){this.enabled=!1;this.timer&&(clearTimeout(this.timer),this.timer=void 0)};
og.prototype.tick=function(){var a=this;if(this.enabled){var b=Math.max(this.h()-this.i,0);b<this.intervalMs*.8?this.timer=setTimeout(function(){a.tick()},this.intervalMs-b):(this.timer&&(clearTimeout(this.timer),this.timer=void 0),this.callback(),this.enabled&&(this.stop(),this.start()))}else this.timer=void 0};function pg(a){this.F=J(a)}
w(pg,L);function qg(a){this.F=J(a)}
w(qg,L);function rg(a,b){this.x=a!==void 0?a:0;this.y=b!==void 0?b:0}
r=rg.prototype;r.clone=function(){return new rg(this.x,this.y)};
r.equals=function(a){return a instanceof rg&&(this==a?!0:this&&a?this.x==a.x&&this.y==a.y:!1)};
r.ceil=function(){this.x=Math.ceil(this.x);this.y=Math.ceil(this.y);return this};
r.floor=function(){this.x=Math.floor(this.x);this.y=Math.floor(this.y);return this};
r.round=function(){this.x=Math.round(this.x);this.y=Math.round(this.y);return this};
r.scale=function(a,b){this.x*=a;this.y*=typeof b==="number"?b:a;return this};function sg(a,b){this.width=a;this.height=b}
r=sg.prototype;r.clone=function(){return new sg(this.width,this.height)};
r.aspectRatio=function(){return this.width/this.height};
r.ceil=function(){this.width=Math.ceil(this.width);this.height=Math.ceil(this.height);return this};
r.floor=function(){this.width=Math.floor(this.width);this.height=Math.floor(this.height);return this};
r.round=function(){this.width=Math.round(this.width);this.height=Math.round(this.height);return this};
r.scale=function(a,b){this.width*=a;this.height*=typeof b==="number"?b:a;return this};function tg(a,b){for(var c in a)b.call(void 0,a[c],c,a)}
function ug(a){var b=vg,c;for(c in b)if(a.call(void 0,b[c],c,b))return c}
function wg(a){for(var b in a)return!1;return!0}
function xg(a,b){if(a!==null&&b in a)throw Error('The object already contains the key "'+b+'"');a[b]=!0}
function yg(a){return a!==null&&"privembed"in a?a.privembed:!1}
function zg(a,b){for(var c in a)if(!(c in b)||a[c]!==b[c])return!1;for(var d in b)if(!(d in a))return!1;return!0}
function Ag(a){var b={},c;for(c in a)b[c]=a[c];return b}
function Bg(a){if(!a||typeof a!=="object")return a;if(typeof a.clone==="function")return a.clone();if(typeof Map!=="undefined"&&a instanceof Map)return new Map(a);if(typeof Set!=="undefined"&&a instanceof Set)return new Set(a);if(a instanceof Date)return new Date(a.getTime());var b=Array.isArray(a)?[]:typeof ArrayBuffer!=="function"||typeof ArrayBuffer.isView!=="function"||!ArrayBuffer.isView(a)||a instanceof DataView?{}:new a.constructor(a.length),c;for(c in a)b[c]=Bg(a[c]);return b}
var Cg="constructor hasOwnProperty isPrototypeOf propertyIsEnumerable toLocaleString toString valueOf".split(" ");function Dg(a,b){for(var c,d,e=1;e<arguments.length;e++){d=arguments[e];for(c in d)a[c]=d[c];for(var f=0;f<Cg.length;f++)c=Cg[f],Object.prototype.hasOwnProperty.call(d,c)&&(a[c]=d[c])}}
;function Eg(a,b){this.h=a===Fg&&b||""}
Eg.prototype.toString=function(){return this.h};
var Fg={};new Eg(Fg,"");"ARTICLE SECTION NAV ASIDE H1 H2 H3 H4 H5 H6 HEADER FOOTER ADDRESS P HR PRE BLOCKQUOTE OL UL LH LI DL DT DD FIGURE FIGCAPTION MAIN DIV EM STRONG SMALL S CITE Q DFN ABBR RUBY RB RT RTC RP DATA TIME CODE VAR SAMP KBD SUB SUP I B U MARK BDI BDO SPAN BR WBR NOBR INS DEL PICTURE PARAM TRACK MAP TABLE CAPTION COLGROUP COL TBODY THEAD TFOOT TR TD TH SELECT DATALIST OPTGROUP OPTION OUTPUT PROGRESS METER FIELDSET LEGEND DETAILS SUMMARY MENU DIALOG SLOT CANVAS FONT CENTER ACRONYM BASEFONT BIG DIR HGROUP STRIKE TT".split(" ").concat(["BUTTON",
"INPUT"]);function Gg(a){var b=document;return typeof a==="string"?b.getElementById(a):a}
function Hg(a){var b=document;a=String(a);b.contentType==="application/xhtml+xml"&&(a=a.toLowerCase());return b.createElement(a)}
function Ig(a){a&&a.parentNode&&a.parentNode.removeChild(a)}
function Jg(a,b){for(var c=0;a;){if(b(a))return a;a=a.parentNode;c++}return null}
;function Kg(a){this.F=J(a)}
w(Kg,L);Kg.prototype.lc=function(){return Af(this)};
function Lg(a,b){return gf(a,2,De(b))}
;function Mg(a){this.F=J(a)}
w(Mg,L);function Ng(a){this.F=J(a)}
w(Ng,L);function Og(a,b){vf(a,Mg,1,b)}
;function lf(a){this.F=J(a)}
w(lf,L);var Pg=["platform","platformVersion","architecture","model","uaFullVersion"],Qg=new Ng,Rg=null;function Sg(a,b){b=b===void 0?Pg:b;if(!Rg){var c;a=(c=a.navigator)==null?void 0:c.userAgentData;if(!a||typeof a.getHighEntropyValues!=="function"||a.brands&&typeof a.brands.map!=="function")return Promise.reject(Error("UACH unavailable"));c=(a.brands||[]).map(function(e){var f=new Mg;f=Bf(f,1,e.brand);return Bf(f,2,e.version)});
Og(gf(Qg,2,De(a.mobile)),c);Rg=a.getHighEntropyValues(b)}var d=new Set(b);return Rg.then(function(e){var f=Qg.clone();d.has("platform")&&Bf(f,3,e.platform);d.has("platformVersion")&&Bf(f,4,e.platformVersion);d.has("architecture")&&Bf(f,5,e.architecture);d.has("model")&&Bf(f,6,e.model);d.has("uaFullVersion")&&Bf(f,7,e.uaFullVersion);return f}).catch(function(){return Qg.clone()})}
;function Tg(a){this.F=J(a)}
w(Tg,L);function Ug(a){this.F=J(a,4)}
w(Ug,L);function Vg(a){this.F=J(a,36)}
w(Vg,L);function Wg(a){this.F=J(a,19)}
w(Wg,L);Wg.prototype.Wb=function(a){return Df(this,2,a)};function Xg(a,b){this.Wa=b=b===void 0?!1:b;this.i=this.locale=null;this.h=new Wg;Number.isInteger(a)&&this.h.Wb(a);b||(this.locale=document.documentElement.getAttribute("lang"));Yg(this,new Tg)}
Xg.prototype.Wb=function(a){this.h.Wb(a);return this};
function Yg(a,b){uf(a.h,Tg,1,b);Af(b)||Df(b,1,1);a.Wa||(b=Zg(a),zf(b,5)||Bf(b,5,a.locale));a.i&&(b=Zg(a),tf(b,Ng,9)||uf(b,Ng,9,a.i))}
function $g(a,b){jf(ah(a))&&(a=bh(a),Df(a,1,b))}
function ah(a){return tf(a.h,Tg,1)}
function ch(a){var b=b===void 0?Pg:b;var c=a.Wa?void 0:window;c?Sg(c,b).then(function(d){a.i=d;d=Zg(a);uf(d,Ng,9,a.i);return!0}).catch(function(){return!1}):Promise.resolve(!1)}
function Zg(a){a=ah(a);var b=tf(a,lf,11);b||(b=new lf,uf(a,lf,11,b));return b}
function bh(a){a=Zg(a);var b=tf(a,Kg,10);b||(b=new Kg,Lg(b,!1),uf(a,Kg,10,b));return b}
function dh(a,b,c,d,e,f,g){c=c===void 0?0:c;d=d===void 0?0:d;e=e===void 0?null:e;f=f===void 0?0:f;g=g===void 0?0:g;if(jf(ah(a))){var h=Zg(a),k=new Kg;var l=bh(a);var m;l=(m=yf(l))!=null?m:void 0;k=Df(k,1,l);m=bh(a);m=ef(m,2);m=m==null||typeof m==="boolean"?m:typeof m==="number"?!!m:void 0;k=Lg(k,m!=null?m:void 0);m=k.F;l=m[I]|0;k=l&2?k:new k.constructor(cf(m,l,!0));uf(h,Kg,10,k)}jf(ah(a))&&d>0&&(h=bh(a),gf(h,3,He(d)));jf(ah(a))&&f>0&&(d=bh(a),gf(d,4,He(f)));jf(ah(a))&&g>0&&(f=bh(a),gf(f,5,He(g)));
a=a.h.clone();g=Date.now().toString();a=gf(a,4,Oe(g));b=b.slice();b=vf(a,Vg,3,b);e&&(a=new pg,e=gf(a,13,He(e)),a=new qg,e=uf(a,pg,2,e),a=new Ug,e=uf(a,qg,1,e),e=Df(e,2,9),uf(b,Ug,18,e));c&&gf(b,14,Oe(c));return b}
;var eh=function(){if(!C.addEventListener||!Object.defineProperty)return!1;var a=!1,b=Object.defineProperty({},"passive",{get:function(){a=!0}});
try{var c=function(){};
C.addEventListener("test",c,b);C.removeEventListener("test",c,b)}catch(d){}return a}();function fh(a){this.h=this.i=this.j=a}
fh.prototype.reset=function(){this.h=this.i=this.j};
fh.prototype.getValue=function(){return this.i};function gh(a){this.F=J(a,8)}
w(gh,L);var hh=Tf(gh);function ih(a){this.F=J(a)}
w(ih,L);var lh=new function(){this.ctor=ih;this.isRepeated=0;this.h=tf;this.defaultValue=void 0};function mh(a){F.call(this);var b=this;this.componentId="";this.h=[];this.Pa="";this.pageId=null;this.Qa=this.ha=-1;this.G=this.experimentIds=null;this.Y=this.Z=this.D=this.o=0;this.rb=1;this.timeoutMillis=0;this.pa=!1;this.logSource=a.logSource;this.hb=a.hb||function(){};
this.j=new Xg(a.logSource,a.Wa);this.network=a.network||null;this.mb=a.mb||null;this.bufferSize=1E3;this.P=a.Af||null;this.sessionIndex=a.sessionIndex||null;this.Ob=a.Ob||!1;this.logger=null;this.withCredentials=!a.rd;this.Wa=a.Wa||!1;this.U=!this.Wa&&!!window&&!!window.navigator&&window.navigator.sendBeacon!==void 0;this.Fa=typeof URLSearchParams!=="undefined"&&!!(new URL(nh())).searchParams&&!!(new URL(nh())).searchParams.set;var c=Df(new Tg,1,1);Yg(this.j,c);this.u=new fh(1E4);a=oh(this,a.md);
this.i=new og(this.u.getValue(),a);this.xa=new og(6E5,a);this.Ob||this.xa.start();this.Wa||(document.addEventListener("visibilitychange",function(){document.visibilityState==="hidden"&&b.Kc()}),document.addEventListener("pagehide",this.Kc.bind(this)))}
w(mh,F);function oh(a,b){return a.Fa?b?function(){b().then(function(){a.flush()})}:function(){a.flush()}:function(){}}
r=mh.prototype;r.ba=function(){this.Kc();this.i.stop();this.xa.stop();F.prototype.ba.call(this)};
function ph(a){a.P||(a.P=nh());try{return(new URL(a.P)).toString()}catch(b){return(new URL(a.P,window.location.origin)).toString()}}
r.log=function(a){if(this.Fa){a=a.clone();var b=this.rb++;a=gf(a,21,Oe(b));this.componentId&&Bf(a,26,this.componentId);b=a;if(wf(b)==null){var c=Date.now();c=Number.isFinite(c)?c.toString():"0";gf(b,1,Oe(c))}Qe(ef(b,15))==null&&gf(b,15,Oe((new Date).getTimezoneOffset()*60));this.experimentIds&&(c=this.experimentIds.clone(),uf(b,ng,16,c));b=this.h.length-this.bufferSize+1;b>0&&(this.h.splice(0,b),this.o+=b);this.h.push(a);this.Ob||this.i.enabled||this.i.start()}};
r.flush=function(a,b){var c=this;if(this.h.length===0)a&&a();else if(this.pa&&this.U)$g(this.j,3),qh(this);else{var d=Date.now();if(this.Qa>d&&this.ha<d)b&&b("throttled");else{this.network&&(typeof this.network.lc==="function"?$g(this.j,this.network.lc()):$g(this.j,0));var e=dh(this.j,this.h,this.o,this.D,this.mb,this.Z,this.Y),f=this.hb();if(f&&this.Pa===f)b&&b("stale-auth-token");else{this.h=[];this.i.enabled&&this.i.stop();this.o=0;d=e.serialize();var g;this.G&&this.G.isSupported(d.length)&&(g=
this.G.compress(d));var h=rh(this,d,f),k=function(n){c.u.reset();c.i.setInterval(c.u.getValue());if(n){var p=null;try{var t=JSON.stringify(JSON.parse(n.replace(")]}'\n","")));p=hh(t)}catch(z){}if(p){n=Number;var u="-1";u=u===void 0?"0":u;var x;t=(x=wf(p))!=null?x:u;x=n(t);x>0&&(c.ha=Date.now(),c.Qa=c.ha+x);p=lh.ctor?lh.h(p,lh.ctor,175237375,!0):lh.h(p,175237375,null,!0);if(p=p===null?void 0:p)p=wc(p,1,-1),p!==-1&&(c.u=new fh(p<1?1:p),c.i.setInterval(c.u.getValue()))}}a&&a();c.D=0},l=function(n,p){var t=
uc(e,Vg,3);
var u;var x=(u=Qe(ef(e,14)))!=null?u:void 0;u=c.u;u.h=Math.min(3E5,u.h*2);u.i=Math.min(3E5,u.h+Math.round(.1*(Math.random()-.5)*2*u.h));c.i.setInterval(c.u.getValue());n===401&&f&&(c.Pa=f);x&&(c.o+=x);p===void 0&&(p=c.isRetryable(n));p&&(c.h=t.concat(c.h),c.Ob||c.i.enabled||c.i.start());b&&b("net-send-failed",n);++c.D},m=function(){c.network&&c.network.send(h,k,l)};
g?g.then(function(n){h.Bc["Content-Encoding"]="gzip";h.Bc["Content-Type"]="application/binary";h.body=n;h.de=2;m()},function(){m()}):m()}}}};
function rh(a,b,c){c=c===void 0?a.hb():c;var d={},e=new URL(ph(a));c&&(d.Authorization=c);a.sessionIndex&&(d["X-Goog-AuthUser"]=a.sessionIndex,e.searchParams.set("authuser",a.sessionIndex));a.pageId&&(Object.defineProperty(d,"X-Goog-PageId",{value:a.pageId}),e.searchParams.set("pageId",a.pageId));return{url:e.toString(),body:b,de:1,Bc:d,requestType:"POST",withCredentials:a.withCredentials,timeoutMillis:a.timeoutMillis}}
r.Kc=function(){var a=this.j;jf(ah(a))&&Lg(bh(a),!0);this.flush();a=this.j;jf(ah(a))&&Lg(bh(a),!1)};
function qh(a){sh(a,function(b,c){b=new URL(b);b.searchParams.set("format","json");var d=!1;try{d=window.navigator.sendBeacon(b.toString(),c.serialize())}catch(e){}d||(a.U=!1);return d})}
function sh(a,b){if(a.h.length!==0){var c=new URL(ph(a));c.searchParams.delete("format");var d=a.hb();d&&c.searchParams.set("auth",d);c.searchParams.set("authuser",a.sessionIndex||"0");for(d=0;d<10&&a.h.length;++d){var e=a.h.slice(0,32),f=dh(a.j,e,a.o,a.D,a.mb,a.Z,a.Y);if(!b(c.toString(),f)){++a.D;break}a.o=0;a.D=0;a.Z=0;a.Y=0;a.h=a.h.slice(e.length)}a.i.enabled&&a.i.stop()}}
r.isRetryable=function(a){return 500<=a&&a<600||a===401||a===0};
function nh(){return"https://play.google.com/log?format=json&hasfast=true"}
;function th(){this.Xd=typeof AbortController!=="undefined"}
th.prototype.send=function(a,b,c){var d=this,e,f,g,h,k,l,m,n,p,t;return A(function(u){switch(u.h){case 1:return f=(e=d.Xd?new AbortController:void 0)?setTimeout(function(){e.abort()},a.timeoutMillis):void 0,za(u,2,3),g=Object.assign({},{method:a.requestType,
headers:Object.assign({},a.Bc)},a.body&&{body:a.body},a.withCredentials&&{credentials:"include"},{signal:a.timeoutMillis&&e?e.signal:null}),u.yield(fetch(a.url,g),5);case 5:h=u.i;if(h.status!==200){(k=c)==null||k(h.status);u.A(3);break}if((l=b)==null){u.A(7);break}return u.yield(h.text(),8);case 8:l(u.i);case 7:case 3:u.P=[u.j];u.M=0;u.o=0;clearTimeout(f);Ca(u);break;case 2:m=Ba(u);switch((n=m)==null?void 0:n.name){case "AbortError":(p=c)==null||p(408);break;default:(t=c)==null||t(400)}u.A(3)}})};
th.prototype.lc=function(){return 4};function uh(a,b){F.call(this);this.logSource=a;this.sessionIndex=b;this.Ua="https://play.google.com/log?format=json&hasfast=true";this.i=null;this.o=!1;this.network=null;this.componentId="";this.h=this.mb=null;this.j=!1;this.pageId=null;this.bufferSize=void 0}
w(uh,F);function vh(a,b){a.i=b;return a}
function wh(a,b){a.network=b;return a}
function xh(a,b){a.h=b}
uh.prototype.rd=function(){this.u=!0;return this};
function yh(a){a.network||(a.network=new th);var b=new mh({logSource:a.logSource,hb:a.hb?a.hb:lg,sessionIndex:a.sessionIndex,Af:a.Ua,Wa:a.o,Ob:!1,rd:a.u,md:a.md,network:a.network});Bc(a,b);if(a.i){var c=a.i,d=Zg(b.j);Bf(d,7,c)}b.G=new mg;a.componentId&&(b.componentId=a.componentId);a.mb&&(b.mb=a.mb);a.pageId&&(b.pageId=a.pageId);a.h&&((d=a.h)?(b.experimentIds||(b.experimentIds=new ng),c=b.experimentIds,d=d.serialize(),Bf(c,4,d)):b.experimentIds&&gf(b.experimentIds,4));a.j&&(b.pa=b.U);ch(b.j);a.bufferSize&&
(b.bufferSize=a.bufferSize);a.network.Wb&&a.network.Wb(a.logSource);a.network.pf&&a.network.pf(b);return b}
;function zh(a,b,c,d,e,f,g){a=a===void 0?-1:a;b=b===void 0?"":b;c=c===void 0?"":c;d=d===void 0?!1:d;e=e===void 0?"":e;F.call(this);this.logSource=a;this.componentId=b;f?b=f:(a=new uh(a,"0"),a.componentId=b,Bc(this,a),c!==""&&(a.Ua=c),d&&(a.o=!0),e&&vh(a,e),g&&wh(a,g),b=yh(a));this.h=b}
w(zh,F);
zh.prototype.flush=function(a){var b=a||[];if(b.length){a=new bg;for(var c=[],d=0;d<b.length;d++){var e=b[d],f=new ag;f=Bf(f,1,e.i);var g=Ah(e);f=nf(f,g,Re);g=[];var h=[];for(var k=y(e.h.keys()),l=k.next();!l.done;l=k.next())h.push(l.value.split(","));for(k=0;k<h.length;k++){l=h[k];var m=e.o;for(var n=e.Lc(l)||[],p=[],t=0;t<n.length;t++){var u=n[t],x=u&&u.h;u=new Yf;switch(m){case 3:x=Number(x);Number.isFinite(x)&&qf(u,1,Zf,Oe(x));break;case 2:x=Number(x);if(x!=null&&typeof x!=="number")throw Error("Value of float/double field must be a number, found "+typeof x+
": "+x);qf(u,2,Zf,x)}p.push(u)}m=p;for(n=0;n<m.length;n++){p=m[n];t=new $f;p=uf(t,Yf,2,p);t=l;u=[];x=Bh(e);for(var z=0;z<x.length;z++){var H=x[z],K=t[z],da=new Wf;switch(H){case 3:qf(da,1,Xf,Se(String(K)));break;case 2:H=Number(K);Number.isFinite(H)&&qf(da,2,Xf,He(H));break;case 1:qf(da,3,Xf,De(K==="true"))}u.push(da)}vf(p,Wf,1,u);g.push(p)}}vf(f,$f,4,g);c.push(f);e.clear()}vf(a,ag,1,c);b=this.h;if(a instanceof Vg)b.log(a);else try{var ea=new Vg,Oa=a.serialize();var Ob=Bf(ea,8,Oa);b.log(Ob)}catch(ka){}this.h.flush()}};function Ch(a){this.h=a}
;function Dh(a,b,c){this.i=a;this.o=b;this.fields=c||[];this.h=new Map}
function Bh(a){return a.fields.map(function(b){return b.fieldType})}
function Ah(a){return a.fields.map(function(b){return b.fieldName})}
r=Dh.prototype;r.Yd=function(a){var b=B.apply(1,arguments),c=this.Lc(b);c?c.push(new Ch(a)):this.Jd(a,b)};
r.Jd=function(a){var b=this.ld(B.apply(1,arguments));this.h.set(b,[new Ch(a)])};
r.Lc=function(){var a=this.ld(B.apply(0,arguments));return this.h.has(a)?this.h.get(a):void 0};
r.xe=function(){var a=this.Lc(B.apply(0,arguments));return a&&a.length?a[0]:void 0};
r.clear=function(){this.h.clear()};
r.ld=function(){var a=B.apply(0,arguments);return a?a.join(","):"key"};function Eh(a,b){Dh.call(this,a,3,b)}
w(Eh,Dh);Eh.prototype.j=function(a){var b=B.apply(1,arguments),c=0,d=this.xe(b);d&&(c=d.h);this.Jd(c+a,b)};function Fh(a,b){Dh.call(this,a,2,b)}
w(Fh,Dh);Fh.prototype.record=function(a){this.Yd(a,B.apply(1,arguments))};function Gh(a,b){this.type=a;this.h=this.target=b;this.defaultPrevented=this.j=!1}
Gh.prototype.stopPropagation=function(){this.j=!0};
Gh.prototype.preventDefault=function(){this.defaultPrevented=!0};function Hh(a,b){Gh.call(this,a?a.type:"");this.relatedTarget=this.h=this.target=null;this.button=this.screenY=this.screenX=this.clientY=this.clientX=0;this.key="";this.charCode=this.keyCode=0;this.metaKey=this.shiftKey=this.altKey=this.ctrlKey=!1;this.state=null;this.pointerId=0;this.pointerType="";this.i=null;a&&this.init(a,b)}
cb(Hh,Gh);
Hh.prototype.init=function(a,b){var c=this.type=a.type,d=a.changedTouches&&a.changedTouches.length?a.changedTouches[0]:null;this.target=a.target||a.srcElement;this.h=b;b=a.relatedTarget;b||(c=="mouseover"?b=a.fromElement:c=="mouseout"&&(b=a.toElement));this.relatedTarget=b;d?(this.clientX=d.clientX!==void 0?d.clientX:d.pageX,this.clientY=d.clientY!==void 0?d.clientY:d.pageY,this.screenX=d.screenX||0,this.screenY=d.screenY||0):(this.clientX=a.clientX!==void 0?a.clientX:a.pageX,this.clientY=a.clientY!==
void 0?a.clientY:a.pageY,this.screenX=a.screenX||0,this.screenY=a.screenY||0);this.button=a.button;this.keyCode=a.keyCode||0;this.key=a.key||"";this.charCode=a.charCode||(c=="keypress"?a.keyCode:0);this.ctrlKey=a.ctrlKey;this.altKey=a.altKey;this.shiftKey=a.shiftKey;this.metaKey=a.metaKey;this.pointerId=a.pointerId||0;this.pointerType=a.pointerType;this.state=a.state;this.i=a;a.defaultPrevented&&Hh.Aa.preventDefault.call(this)};
Hh.prototype.stopPropagation=function(){Hh.Aa.stopPropagation.call(this);this.i.stopPropagation?this.i.stopPropagation():this.i.cancelBubble=!0};
Hh.prototype.preventDefault=function(){Hh.Aa.preventDefault.call(this);var a=this.i;a.preventDefault?a.preventDefault():a.returnValue=!1};var Ih="closure_listenable_"+(Math.random()*1E6|0);var Jh=0;function Kh(a,b,c,d,e){this.listener=a;this.proxy=null;this.src=b;this.type=c;this.capture=!!d;this.oc=e;this.key=++Jh;this.Vb=this.ec=!1}
function Lh(a){a.Vb=!0;a.listener=null;a.proxy=null;a.src=null;a.oc=null}
;function Mh(a){this.src=a;this.listeners={};this.h=0}
Mh.prototype.add=function(a,b,c,d,e){var f=a.toString();a=this.listeners[f];a||(a=this.listeners[f]=[],this.h++);var g=Nh(a,b,d,e);g>-1?(b=a[g],c||(b.ec=!1)):(b=new Kh(b,this.src,f,!!d,e),b.ec=c,a.push(b));return b};
Mh.prototype.remove=function(a,b,c,d){a=a.toString();if(!(a in this.listeners))return!1;var e=this.listeners[a];b=Nh(e,b,c,d);return b>-1?(Lh(e[b]),Array.prototype.splice.call(e,b,1),e.length==0&&(delete this.listeners[a],this.h--),!0):!1};
function Oh(a,b){var c=b.type;c in a.listeners&&Xb(a.listeners[c],b)&&(Lh(b),a.listeners[c].length==0&&(delete a.listeners[c],a.h--))}
function Nh(a,b,c,d){for(var e=0;e<a.length;++e){var f=a[e];if(!f.Vb&&f.listener==b&&f.capture==!!c&&f.oc==d)return e}return-1}
;var Ph="closure_lm_"+(Math.random()*1E6|0),Qh={},Rh=0;function Sh(a,b,c,d,e){if(d&&d.once)Th(a,b,c,d,e);else if(Array.isArray(b))for(var f=0;f<b.length;f++)Sh(a,b[f],c,d,e);else c=Uh(c),a&&a[Ih]?a.listen(b,c,Sa(d)?!!d.capture:!!d,e):Vh(a,b,c,!1,d,e)}
function Vh(a,b,c,d,e,f){if(!b)throw Error("Invalid event type");var g=Sa(e)?!!e.capture:!!e,h=Wh(a);h||(a[Ph]=h=new Mh(a));c=h.add(b,c,d,g,f);if(!c.proxy){d=Xh();c.proxy=d;d.src=a;d.listener=c;if(a.addEventListener)eh||(e=g),e===void 0&&(e=!1),a.addEventListener(b.toString(),d,e);else if(a.attachEvent)a.attachEvent(Yh(b.toString()),d);else if(a.addListener&&a.removeListener)a.addListener(d);else throw Error("addEventListener and attachEvent are unavailable.");Rh++}}
function Xh(){function a(c){return b.call(a.src,a.listener,c)}
var b=Zh;return a}
function Th(a,b,c,d,e){if(Array.isArray(b))for(var f=0;f<b.length;f++)Th(a,b[f],c,d,e);else c=Uh(c),a&&a[Ih]?$h(a,b,c,Sa(d)?!!d.capture:!!d,e):Vh(a,b,c,!0,d,e)}
function ai(a,b,c,d,e){if(Array.isArray(b))for(var f=0;f<b.length;f++)ai(a,b[f],c,d,e);else(d=Sa(d)?!!d.capture:!!d,c=Uh(c),a&&a[Ih])?a.i.remove(String(b),c,d,e):a&&(a=Wh(a))&&(b=a.listeners[b.toString()],a=-1,b&&(a=Nh(b,c,d,e)),(c=a>-1?b[a]:null)&&bi(c))}
function bi(a){if(typeof a!=="number"&&a&&!a.Vb){var b=a.src;if(b&&b[Ih])Oh(b.i,a);else{var c=a.type,d=a.proxy;b.removeEventListener?b.removeEventListener(c,d,a.capture):b.detachEvent?b.detachEvent(Yh(c),d):b.addListener&&b.removeListener&&b.removeListener(d);Rh--;(c=Wh(b))?(Oh(c,a),c.h==0&&(c.src=null,b[Ph]=null)):Lh(a)}}}
function Yh(a){return a in Qh?Qh[a]:Qh[a]="on"+a}
function Zh(a,b){if(a.Vb)a=!0;else{b=new Hh(b,this);var c=a.listener,d=a.oc||a.src;a.ec&&bi(a);a=c.call(d,b)}return a}
function Wh(a){a=a[Ph];return a instanceof Mh?a:null}
var ci="__closure_events_fn_"+(Math.random()*1E9>>>0);function Uh(a){if(typeof a==="function")return a;a[ci]||(a[ci]=function(b){return a.handleEvent(b)});
return a[ci]}
;function di(){F.call(this);this.i=new Mh(this);this.xa=this;this.Z=null}
cb(di,F);di.prototype[Ih]=!0;r=di.prototype;r.addEventListener=function(a,b,c,d){Sh(this,a,b,c,d)};
r.removeEventListener=function(a,b,c,d){ai(this,a,b,c,d)};
function ei(a,b){var c=a.Z;if(c){var d=[];for(var e=1;c;c=c.Z)d.push(c),++e}a=a.xa;c=b.type||b;typeof b==="string"?b=new Gh(b,a):b instanceof Gh?b.target=b.target||a:(e=b,b=new Gh(c,a),Dg(b,e));e=!0;var f;if(d)for(f=d.length-1;!b.j&&f>=0;f--){var g=b.h=d[f];e=fi(g,c,!0,b)&&e}b.j||(g=b.h=a,e=fi(g,c,!0,b)&&e,b.j||(e=fi(g,c,!1,b)&&e));if(d)for(f=0;!b.j&&f<d.length;f++)g=b.h=d[f],e=fi(g,c,!1,b)&&e}
r.ba=function(){di.Aa.ba.call(this);this.removeAllListeners();this.Z=null};
r.listen=function(a,b,c,d){return this.i.add(String(a),b,!1,c,d)};
function $h(a,b,c,d,e){a.i.add(String(b),c,!0,d,e)}
r.removeAllListeners=function(a){if(this.i){var b=this.i;a=a&&a.toString();var c=0,d;for(d in b.listeners)if(!a||d==a){for(var e=b.listeners[d],f=0;f<e.length;f++)++c,Lh(e[f]);delete b.listeners[d];b.h--}b=c}else b=0;return b};
function fi(a,b,c,d){b=a.i.listeners[String(b)];if(!b)return!0;b=b.concat();for(var e=!0,f=0;f<b.length;++f){var g=b[f];if(g&&!g.Vb&&g.capture==c){var h=g.listener,k=g.oc||g.src;g.ec&&Oh(a.i,g);e=h.call(k,d)!==!1&&e}}return e&&!d.defaultPrevented}
;var gi=typeof AsyncContext!=="undefined"&&typeof AsyncContext.Snapshot==="function"?function(a){return a&&AsyncContext.Snapshot.wrap(a)}:function(a){return a};function hi(a,b){this.j=a;this.o=b;this.i=0;this.h=null}
hi.prototype.get=function(){if(this.i>0){this.i--;var a=this.h;this.h=a.next;a.next=null}else a=this.j();return a};
function ii(a,b){a.o(b);a.i<100&&(a.i++,b.next=a.h,a.h=b)}
;function ji(){this.i=this.h=null}
ji.prototype.add=function(a,b){var c=ki.get();c.set(a,b);this.i?this.i.next=c:this.h=c;this.i=c};
ji.prototype.remove=function(){var a=null;this.h&&(a=this.h,this.h=this.h.next,this.h||(this.i=null),a.next=null);return a};
var ki=new hi(function(){return new li},function(a){return a.reset()});
function li(){this.next=this.scope=this.h=null}
li.prototype.set=function(a,b){this.h=a;this.scope=b;this.next=null};
li.prototype.reset=function(){this.next=this.scope=this.h=null};var mi,ni=!1,oi=new ji;function pi(a,b){mi||qi();ni||(mi(),ni=!0);oi.add(a,b)}
function qi(){var a=Promise.resolve(void 0);mi=function(){a.then(ri)}}
function ri(){for(var a;a=oi.remove();){try{a.h.call(a.scope)}catch(b){Oc(b)}ii(ki,a)}ni=!1}
;function si(){}
function ti(a){var b=!1,c;return function(){b||(c=a(),b=!0);return c}}
;function ui(a){this.X=0;this.ab=void 0;this.ub=this.Sa=this.parent_=null;this.nc=this.Jc=!1;if(a!=si)try{var b=this;a.call(void 0,function(c){vi(b,2,c)},function(c){vi(b,3,c)})}catch(c){vi(this,3,c)}}
function wi(){this.next=this.context=this.h=this.i=this.child=null;this.j=!1}
wi.prototype.reset=function(){this.context=this.h=this.i=this.child=null;this.j=!1};
var xi=new hi(function(){return new wi},function(a){a.reset()});
function yi(a,b,c){var d=xi.get();d.i=a;d.h=b;d.context=c;return d}
function zi(a){return new ui(function(b,c){c(a)})}
ui.prototype.then=function(a,b,c){return Ai(this,gi(typeof a==="function"?a:null),gi(typeof b==="function"?b:null),c)};
ui.prototype.$goog_Thenable=!0;function Bi(a,b,c,d){Ci(a,yi(b||si,c||null,d))}
r=ui.prototype;r.finally=function(a){var b=this;a=gi(a);return new Promise(function(c,d){Bi(b,function(e){a();c(e)},function(e){a();
d(e)})})};
r.Dc=function(a,b){return Ai(this,null,gi(a),b)};
r.catch=ui.prototype.Dc;r.cancel=function(a){if(this.X==0){var b=new Di(a);pi(function(){Ei(this,b)},this)}};
function Ei(a,b){if(a.X==0)if(a.parent_){var c=a.parent_;if(c.Sa){for(var d=0,e=null,f=null,g=c.Sa;g&&(g.j||(d++,g.child==a&&(e=g),!(e&&d>1)));g=g.next)e||(f=g);e&&(c.X==0&&d==1?Ei(c,b):(f?(d=f,d.next==c.ub&&(c.ub=d),d.next=d.next.next):Fi(c),Gi(c,e,3,b)))}a.parent_=null}else vi(a,3,b)}
function Ci(a,b){a.Sa||a.X!=2&&a.X!=3||Hi(a);a.ub?a.ub.next=b:a.Sa=b;a.ub=b}
function Ai(a,b,c,d){var e=yi(null,null,null);e.child=new ui(function(f,g){e.i=b?function(h){try{var k=b.call(d,h);f(k)}catch(l){g(l)}}:f;
e.h=c?function(h){try{var k=c.call(d,h);k===void 0&&h instanceof Di?g(h):f(k)}catch(l){g(l)}}:g});
e.child.parent_=a;Ci(a,e);return e.child}
r.yf=function(a){this.X=0;vi(this,2,a)};
r.zf=function(a){this.X=0;vi(this,3,a)};
function vi(a,b,c){if(a.X==0){a===c&&(b=3,c=new TypeError("Promise cannot resolve to itself"));a.X=1;a:{var d=c,e=a.yf,f=a.zf;if(d instanceof ui){Bi(d,e,f,a);var g=!0}else{if(d)try{var h=!!d.$goog_Thenable}catch(l){h=!1}else h=!1;if(h)d.then(e,f,a),g=!0;else{if(Sa(d))try{var k=d.then;if(typeof k==="function"){Ii(d,k,e,f,a);g=!0;break a}}catch(l){f.call(a,l);g=!0;break a}g=!1}}}g||(a.ab=c,a.X=b,a.parent_=null,Hi(a),b!=3||c instanceof Di||Ji(a,c))}}
function Ii(a,b,c,d,e){function f(k){h||(h=!0,d.call(e,k))}
function g(k){h||(h=!0,c.call(e,k))}
var h=!1;try{b.call(a,g,f)}catch(k){f(k)}}
function Hi(a){a.Jc||(a.Jc=!0,pi(a.re,a))}
function Fi(a){var b=null;a.Sa&&(b=a.Sa,a.Sa=b.next,b.next=null);a.Sa||(a.ub=null);return b}
r.re=function(){for(var a;a=Fi(this);)Gi(this,a,this.X,this.ab);this.Jc=!1};
function Gi(a,b,c,d){if(c==3&&b.h&&!b.j)for(;a&&a.nc;a=a.parent_)a.nc=!1;if(b.child)b.child.parent_=null,Ki(b,c,d);else try{b.j?b.i.call(b.context):Ki(b,c,d)}catch(e){Li.call(null,e)}ii(xi,b)}
function Ki(a,b,c){b==2?a.i.call(a.context,c):a.h&&a.h.call(a.context,c)}
function Ji(a,b){a.nc=!0;pi(function(){a.nc&&Li.call(null,b)})}
var Li=Oc;function Di(a){db.call(this,a)}
cb(Di,db);Di.prototype.name="cancel";function Mi(a,b){di.call(this);this.j=a||1;this.h=b||C;this.o=Za(this.uf,this);this.u=ab()}
cb(Mi,di);r=Mi.prototype;r.enabled=!1;r.Ea=null;r.setInterval=function(a){this.j=a;this.Ea&&this.enabled?(this.stop(),this.start()):this.Ea&&this.stop()};
r.uf=function(){if(this.enabled){var a=ab()-this.u;a>0&&a<this.j*.8?this.Ea=this.h.setTimeout(this.o,this.j-a):(this.Ea&&(this.h.clearTimeout(this.Ea),this.Ea=null),ei(this,"tick"),this.enabled&&(this.stop(),this.start()))}};
r.start=function(){this.enabled=!0;this.Ea||(this.Ea=this.h.setTimeout(this.o,this.j),this.u=ab())};
r.stop=function(){this.enabled=!1;this.Ea&&(this.h.clearTimeout(this.Ea),this.Ea=null)};
r.ba=function(){Mi.Aa.ba.call(this);this.stop();delete this.h};function Ni(a){F.call(this);this.G=a;this.j=0;this.o=100;this.u=!1;this.i=new Map;this.D=new Set;this.flushInterval=3E4;this.h=new Mi(this.flushInterval);this.h.listen("tick",this.Yb,!1,this);Bc(this,this.h)}
w(Ni,F);r=Ni.prototype;r.sendIsolatedPayload=function(a){this.u=a;this.o=1};
function Oi(a){a.h.enabled||a.h.start();a.j++;a.j>=a.o&&a.Yb()}
r.Yb=function(){var a=this.i.values();a=[].concat(ra(a)).filter(function(b){return b.h.size});
a.length&&this.G.flush(a,this.u);Pi(a);this.j=0;this.h.enabled&&this.h.stop()};
r.Kb=function(a){var b=B.apply(1,arguments);this.i.has(a)||this.i.set(a,new Eh(a,b))};
r.Gc=function(a){var b=B.apply(1,arguments);this.i.has(a)||this.i.set(a,new Fh(a,b))};
function Qi(a,b){return a.D.has(b)?void 0:a.i.get(b)}
r.Ib=function(a){this.Wd(a,1,B.apply(1,arguments))};
r.Wd=function(a,b){var c=B.apply(2,arguments),d=Qi(this,a);d&&d instanceof Eh&&(d.j(b,c),Oi(this))};
r.record=function(a,b){var c=B.apply(2,arguments),d=Qi(this,a);d&&d instanceof Fh&&(d.record(b,c),Oi(this))};
function Pi(a){for(var b=0;b<a.length;b++)a[b].clear()}
;function Ri(){}
Ri.prototype.serialize=function(a){var b=[];Si(this,a,b);return b.join("")};
function Si(a,b,c){if(b==null)c.push("null");else{if(typeof b=="object"){if(Array.isArray(b)){var d=b;b=d.length;c.push("[");for(var e="",f=0;f<b;f++)c.push(e),Si(a,d[f],c),e=",";c.push("]");return}if(b instanceof String||b instanceof Number||b instanceof Boolean)b=b.valueOf();else{c.push("{");e="";for(d in b)Object.prototype.hasOwnProperty.call(b,d)&&(f=b[d],typeof f!="function"&&(c.push(e),Ti(d,c),c.push(":"),Si(a,f,c),e=","));c.push("}");return}}switch(typeof b){case "string":Ti(b,c);break;case "number":c.push(isFinite(b)&&
!isNaN(b)?String(b):"null");break;case "boolean":c.push(String(b));break;case "function":c.push("null");break;default:throw Error("Unknown type: "+typeof b);}}}
var Ui={'"':'\\"',"\\":"\\\\","/":"\\/","\b":"\\b","\f":"\\f","\n":"\\n","\r":"\\r","\t":"\\t","\v":"\\u000b"},Vi=/\uffff/.test("\uffff")?/[\\"\x00-\x1f\x7f-\uffff]/g:/[\\"\x00-\x1f\x7f-\xff]/g;function Ti(a,b){b.push('"',a.replace(Vi,function(c){var d=Ui[c];d||(d="\\u"+(c.charCodeAt(0)|65536).toString(16).slice(1),Ui[c]=d);return d}),'"')}
;function Wi(){di.call(this);this.headers=new Map;this.h=!1;this.J=null;this.o=this.Y="";this.j=this.U=this.D=this.P=!1;this.G=0;this.u=null;this.pa="";this.ha=!1}
cb(Wi,di);var Xi=/^https?$/i,Yi=["POST","PUT"],Zi=[];function $i(a,b,c,d,e,f,g){var h=new Wi;Zi.push(h);b&&h.listen("complete",b);$h(h,"ready",h.ge);f&&(h.G=Math.max(0,f));g&&(h.ha=g);h.send(a,c,d,e)}
r=Wi.prototype;r.ge=function(){this.dispose();Xb(Zi,this)};
r.send=function(a,b,c,d){if(this.J)throw Error("[goog.net.XhrIo] Object is active with another request="+this.Y+"; newUri="+a);b=b?b.toUpperCase():"GET";this.Y=a;this.o="";this.P=!1;this.h=!0;this.J=new XMLHttpRequest;this.J.onreadystatechange=gi(Za(this.Cd,this));try{this.getStatus(),this.U=!0,this.J.open(b,String(a),!0),this.U=!1}catch(g){this.getStatus();aj(this,g);return}a=c||"";c=new Map(this.headers);if(d)if(Object.getPrototypeOf(d)===Object.prototype)for(var e in d)c.set(e,d[e]);else if(typeof d.keys===
"function"&&typeof d.get==="function"){e=y(d.keys());for(var f=e.next();!f.done;f=e.next())f=f.value,c.set(f,d.get(f))}else throw Error("Unknown input type for opt_headers: "+String(d));d=Array.from(c.keys()).find(function(g){return"content-type"==g.toLowerCase()});
e=C.FormData&&a instanceof C.FormData;!(Rb(Yi,b)>=0)||d||e||c.set("Content-Type","application/x-www-form-urlencoded;charset=utf-8");b=y(c);for(d=b.next();!d.done;d=b.next())c=y(d.value),d=c.next().value,c=c.next().value,this.J.setRequestHeader(d,c);this.pa&&(this.J.responseType=this.pa);"withCredentials"in this.J&&this.J.withCredentials!==this.ha&&(this.J.withCredentials=this.ha);try{this.u&&(clearTimeout(this.u),this.u=null),this.G>0&&(this.getStatus(),this.u=setTimeout(this.xf.bind(this),this.G)),
this.getStatus(),this.D=!0,this.J.send(a),this.D=!1}catch(g){this.getStatus(),aj(this,g)}};
r.xf=function(){typeof La!="undefined"&&this.J&&(this.o="Timed out after "+this.G+"ms, aborting",this.getStatus(),ei(this,"timeout"),this.abort(8))};
function aj(a,b){a.h=!1;a.J&&(a.j=!0,a.J.abort(),a.j=!1);a.o=b;bj(a);cj(a)}
function bj(a){a.P||(a.P=!0,ei(a,"complete"),ei(a,"error"))}
r.abort=function(){this.J&&this.h&&(this.getStatus(),this.h=!1,this.j=!0,this.J.abort(),this.j=!1,ei(this,"complete"),ei(this,"abort"),cj(this))};
r.ba=function(){this.J&&(this.h&&(this.h=!1,this.j=!0,this.J.abort(),this.j=!1),cj(this,!0));Wi.Aa.ba.call(this)};
r.Cd=function(){this.ea||(this.U||this.D||this.j?dj(this):this.Oe())};
r.Oe=function(){dj(this)};
function dj(a){if(a.h&&typeof La!="undefined")if(a.D&&(a.J?a.J.readyState:0)==4)setTimeout(a.Cd.bind(a),0);else if(ei(a,"readystatechange"),a.isComplete()){a.getStatus();a.h=!1;try{if(ej(a))ei(a,"complete"),ei(a,"success");else{try{var b=(a.J?a.J.readyState:0)>2?a.J.statusText:""}catch(c){b=""}a.o=b+" ["+a.getStatus()+"]";bj(a)}}finally{cj(a)}}}
function cj(a,b){if(a.J){a.u&&(clearTimeout(a.u),a.u=null);var c=a.J;a.J=null;b||ei(a,"ready");try{c.onreadystatechange=null}catch(d){}}}
r.isActive=function(){return!!this.J};
r.isComplete=function(){return(this.J?this.J.readyState:0)==4};
function ej(a){var b=a.getStatus();a:switch(b){case 200:case 201:case 202:case 204:case 206:case 304:case 1223:var c=!0;break a;default:c=!1}if(!c){if(b=b===0)a=hc(1,String(a.Y)),!a&&C.self&&C.self.location&&(a=C.self.location.protocol.slice(0,-1)),b=!Xi.test(a?a.toLowerCase():"");c=b}return c}
r.getStatus=function(){try{return(this.J?this.J.readyState:0)>2?this.J.status:-1}catch(a){return-1}};
r.getLastError=function(){return typeof this.o==="string"?this.o:String(this.o)};function fj(){}
fj.prototype.send=function(a,b,c){b=b===void 0?function(){}:b;
c=c===void 0?function(){}:c;
$i(a.url,function(d){d=d.target;if(ej(d)){try{var e=d.J?d.J.responseText:""}catch(f){e=""}b(e)}else c(d.getStatus())},a.requestType,a.body,a.Bc,a.timeoutMillis,a.withCredentials)};
fj.prototype.lc=function(){return 1};function gj(a,b){this.logger=a;this.event=b;this.startTime=hj()}
gj.prototype.done=function(){this.logger.Sb(this.event,hj()-this.startTime)};
function ij(){Dc.apply(this,arguments)}
w(ij,Dc);function jj(a,b){var c=hj();b=b();a.Sb("n",hj()-c);return b}
function kj(){ij.apply(this,arguments)}
w(kj,ij);r=kj.prototype;r.Pc=function(){};
r.Bb=function(){};
r.Sb=function(){};
r.Ha=function(){};
r.Ac=function(){};
r.Od=function(){};
function lj(a){return{sf:new Gc(a),errorCount:new Kc(a),eventCount:new Ic(a),qe:new Jc(a),ai:new Hc(a),ci:new Lc(a),vh:new Mc(a),bi:new Nc(a)}}
function mj(a,b,c,d,e){a=wh(vh(new uh(1828,"0"),a),new fj);b.length&&xh(a,Vf(new Uf,b));e!==void 0&&(a.Ua=e);d&&(a.j=!0);var f=new zh(1828,"","",!1,"",yh(a));Bc(f,a);var g=new Ni({flush:function(h){try{f.flush(h)}catch(k){c(k)}}});
g.addOnDisposeCallback(function(){setTimeout(function(){try{g.Yb()}finally{f.dispose()}})});
g.o=1E5;g.flushInterval=3E4;g.h.setInterval(3E4);return g}
function nj(a,b){F.call(this);var c=this;this.callback=a;this.i=b;this.h=-b;this.addOnDisposeCallback(function(){return void clearTimeout(c.timer)})}
w(nj,F);function oj(a){if(a.timer===void 0){var b=Math.max(0,a.h+a.i-hj());a.timer=setTimeout(function(){try{a.callback()}finally{a.h=hj(),a.timer=void 0}},b)}}
function pj(a,b,c){ij.call(this);this.metrics=a;this.Da=b;this.ob=c}
w(pj,ij);pj.prototype.Pc=function(a){this.metrics.sf.record(a,this.Da)};
pj.prototype.Bb=function(a){this.metrics.eventCount.h(a,this.Da)};
pj.prototype.Sb=function(a,b){this.metrics.qe.record(b,a,this.ob,this.Da)};
pj.prototype.Ha=function(a){this.metrics.errorCount.h(a,this.ob,this.Da)};
function qj(a,b){b=b===void 0?[]:b;var c={Da:a.Da||"_",ob:a.ob||"",kc:a.kc||[],sc:a.sc|0,Ua:a.Ua,uc:a.uc||function(){},
Ic:!!a.Ic,Hb:a.Hb||function(e,f){return mj(e,f,c.uc,c.Ic,c.Ua)}};
b=c.Hb("42",c.kc.concat(b));pj.call(this,lj(b),c.Da,c.ob);var d=this;this.options=c;this.service=b;this.i=!a.Hb;this.h=new nj(function(){return void d.service.Yb()},c.sc);
this.addOnDisposeCallback(function(){d.h.dispose();d.i&&d.service.dispose()})}
w(qj,pj);qj.prototype.Od=function(a){var b=this;this.h.dispose();this.i&&this.service.dispose();this.service=this.options.Hb("42",this.options.kc.concat(a));this.h=new nj(function(){return void b.service.Yb()},this.options.sc);
this.metrics=lj(this.service)};
qj.prototype.Ac=function(){oj(this.h)};
function hj(){var a,b,c;return(c=(a=globalThis.performance)==null?void 0:(b=a.now)==null?void 0:b.call(a))!=null?c:Date.now()}
;function rj(a){this.F=J(a)}
w(rj,L);function sj(a){this.F=J(a)}
w(sj,L);function tj(a){this.F=J(a,0,"bfkj")}
w(tj,L);var uj=function(a){return be(function(b){return b instanceof a&&!((b.F[I]|0)&2)})}(tj);function vj(a){this.F=J(a)}
w(vj,L);function vc(a){this.F=J(a)}
w(vc,L);function wj(a){this.F=J(a)}
w(wj,L);var xj=Tf(wj);function yj(){var a=this;this.promise=new Promise(function(b,c){a.resolve=b;a.reject=c})}
;function zj(a,b,c){if(a.disable)return new kj;var d=b&&yc(tf(b,vj,7))?tc(b):[];if(c)return c.Od(d),c.share();a={Da:a.Da,ob:a.ob,kc:a.Bh,sc:a.Lh,Ic:yc(b==null?void 0:tf(b,vj,10)),Ua:a.Ua,uc:a.uc,Hb:a.Hb};d=d===void 0?[]:d;return new qj(a,d)}
function Aj(a){function b(u,x,z,H){Promise.resolve().then(function(){k.done();h.Ac();h.dispose();g.resolve({ae:u,rf:x,Se:z,xh:H})})}
function c(u,x,z,H){if(!d.logger.ea){var K="k";x?K="h":z&&(K="u");K!=="k"?H!==0&&(d.logger.Bb(K),d.logger.Sb(K,u)):d.i<=0?(d.logger.Bb(K),d.logger.Sb(K,u),d.i=Math.floor(Math.random()*200)):d.i--}}
F.call(this);var d=this;this.i=Math.floor(Math.random()*200);this.h=new wj;if("challenge"in a&&uj(a.challenge)){var e=zf(a.challenge,4);var f=zf(a.challenge,5);zf(a.challenge,7)&&(this.h=xj(zf(a.challenge,7)))}else e=a.program,f=a.globalName;this.addOnDisposeCallback(function(){var u,x,z;return A(function(H){if(H.h==1)return H.yield(d.j,2);u=H.i;x=u.rf;(z=x)==null||z();H.h=0})});
this.logger=zj(a.Ad||{},this.h,a.yh);Bc(this,this.logger);var g=new yj;this.j=g.promise;this.logger.Bb("t");var h=this.logger.share(),k=new gj(h,"t");if(!C[f])throw this.logger.Ha(25),Error("EGOU");if(!C[f].a)throw this.logger.Ha(26),Error("ELIU");try{var l=C[f].a;f=[];var m=[];if(yc(tf(this.h,vj,7))){for(var n=tc(this.h),p=0;p<n.length;p++)f.push(n[p]),m.push(1);var t=xc(this.h);for(n=0;n<t.length;n++)f.push(t[n]),m.push(2)}this.u=y(l(e,b,!0,a.Zh,c,[f,m],zf(this.h,5))).next().value;this.bd=g.promise.then(function(){})}catch(u){throw this.logger.Ha(28),
u;
}}
w(Aj,F);Aj.prototype.snapshot=function(a){if(this.ea)throw Error("Already disposed");this.logger.Bb("n");var b=this.logger.share();return this.j.then(function(c){var d=c.ae;return new Promise(function(e){var f=new gj(b,"n");d(function(g){f.done();b.Pc(g.length);b.Ac();b.dispose();e(g)},[a.vb,
a.dd,a.Cf,a.ed])})})};
Aj.prototype.gd=function(a){var b=this;if(this.ea)throw Error("Already disposed");this.logger.Bb("n");var c=jj(this.logger,function(){return b.u([a.vb,a.dd,a.Cf,a.ed])});
this.logger.Pc(c.length);this.logger.Ac();return c};
Aj.prototype.o=function(a){this.j.then(function(b){var c;(c=b.Se)==null||c(a)})};function Bj(){var a=Cj();a=a===void 0?"bevasrsg":a;return new Promise(function(b){var c=window===window.top?window:sc()?window:window.top,d=c[a],e;((e=d)==null?0:e.bevasrs)?b(new Dj(d.bevasrs)):(d||(d={},d=(d.nqfbel=[],d),c[a]=d),d.nqfbel.push(function(f){b(new Dj(f))}))})}
function Dj(a){F.call(this);var b=this;this.vm=a;this.i="keydown keypress keyup input focusin focusout select copy cut paste change click dblclick auxclick pointerover pointerdown pointerup pointermove pointerout dragenter dragleave drag dragend mouseover mousedown mouseup mousemove mouseout touchstart touchend touchmove wheel".split(" ");this.h=void 0;this.bd=this.vm.p;this.j=this.o.bind(this);this.addOnDisposeCallback(function(){return void Ej(b)})}
w(Dj,F);Dj.prototype.snapshot=function(a){return this.vm.s(Object.assign({},a.vb&&{c:a.vb},a.dd&&{s:a.dd},a.ed!==void 0&&{p:a.ed}))};
Dj.prototype.o=function(a){this.vm.e(a)};
function Ej(a){a.h!==void 0&&(a.i.forEach(function(b){var c;(c=a.h)==null||c.removeEventListener(b,a.j)}),a.h=void 0)}
;function Fj(a){if(!a)return null;a=xf(a,4);return a===null||a===void 0?null:ob(a)}
;function Gj(){this.promises={};this.h=null}
function Hj(){Gj.h||(Gj.h=new Gj);return Gj.h}
function Ij(a,b){return Jj(a,tf(b,rj,1),tf(b,sj,2),zf(b,3))}
function Jj(a,b,c,d){if(!b&&!c)return Promise.resolve();if(!d)return Kj(b,c);var e;(e=a.promises)[d]||(e[d]=new Promise(function(f,g){Kj(b,c).then(function(){a.h=d;f()},function(h){delete a.promises[d];
g(h)})}));
return a.promises[d]}
function Kj(a,b){return b?Lj(b):a?Mj(a):Promise.resolve()}
function Lj(a){return new Promise(function(b,c){var d=Hg("SCRIPT"),e=Fj(a);Kb(d,e);d.onload=function(){Ig(d);b()};
d.onerror=function(){Ig(d);c(Error("EWLS"))};
(document.getElementsByTagName("HEAD")[0]||document.documentElement).appendChild(d)})}
function Mj(a){return new Promise(function(b){var c=Hg("SCRIPT");if(a){var d=xf(a,6);d=d===null||d===void 0?null:Hb(d)}else d=null;c.textContent=Ib(d);Jb(c);(document.getElementsByTagName("HEAD")[0]||document.documentElement).appendChild(c);Ig(c);b()})}
;var Nj=window;function Oj(a){var b=Pj;if(b)for(var c in b)Object.prototype.hasOwnProperty.call(b,c)&&a(b[c],c,b)}
function Qj(){var a=[];Oj(function(b){a.push(b)});
return a}
var Pj={Df:"allow-forms",Ef:"allow-modals",Ff:"allow-orientation-lock",Gf:"allow-pointer-lock",Hf:"allow-popups",If:"allow-popups-to-escape-sandbox",Jf:"allow-presentation",Kf:"allow-same-origin",Lf:"allow-scripts",Mf:"allow-top-navigation",Nf:"allow-top-navigation-by-user-activation"},Rj=ti(function(){return Qj()});
function Sj(){var a=Tj(),b={};Sb(Rj(),function(c){a.sandbox&&a.sandbox.supports&&a.sandbox.supports(c)&&(b[c]=!0)});
return b}
function Tj(){var a=a===void 0?document:a;return a.createElement("iframe")}
;function Uj(a){typeof a=="number"&&(a=Math.round(a)+"px");return a}
;var Vj=(new Date).getTime();function Wj(a){di.call(this);var b=this;this.D=this.j=0;this.Ca=a!=null?a:{ma:function(e,f){return setTimeout(e,f)},
qa:function(e){clearTimeout(e)}};
var c,d;this.h=(d=(c=window.navigator)==null?void 0:c.onLine)!=null?d:!0;this.o=function(){return A(function(e){return e.yield(Xj(b),0)})};
window.addEventListener("offline",this.o);window.addEventListener("online",this.o);this.D||Yj(this)}
w(Wj,di);function Zj(){var a=ak;Wj.h||(Wj.h=new Wj(a));return Wj.h}
Wj.prototype.dispose=function(){window.removeEventListener("offline",this.o);window.removeEventListener("online",this.o);this.Ca.qa(this.D);delete Wj.h};
Wj.prototype.ta=function(){return this.h};
function Yj(a){a.D=a.Ca.ma(function(){var b;return A(function(c){if(c.h==1)return a.h?((b=window.navigator)==null?0:b.onLine)?c.A(3):c.yield(Xj(a),3):c.yield(Xj(a),3);Yj(a);c.h=0})},3E4)}
function Xj(a,b){return a.u?a.u:a.u=new Promise(function(c){var d,e,f,g;return A(function(h){switch(h.h){case 1:return d=window.AbortController?new window.AbortController:void 0,f=(e=d)==null?void 0:e.signal,g=!1,za(h,2,3),d&&(a.j=a.Ca.ma(function(){d.abort()},b||2E4)),h.yield(fetch("/generate_204",{method:"HEAD",
signal:f}),5);case 5:g=!0;case 3:h.P=[h.j];h.M=0;h.o=0;a.u=void 0;a.j&&(a.Ca.qa(a.j),a.j=0);g!==a.h&&(a.h=g,a.h?ei(a,"networkstatus-online"):ei(a,"networkstatus-offline"));c(g);Ca(h);break;case 2:Ba(h),g=!1,h.A(3)}})})}
;function bk(){this.data=[];this.h=-1}
bk.prototype.set=function(a,b){b=b===void 0?!0:b;0<=a&&a<52&&Number.isInteger(a)&&this.data[a]!==b&&(this.data[a]=b,this.h=-1)};
bk.prototype.get=function(a){return!!this.data[a]};
function ck(a){a.h===-1&&(a.h=a.data.reduce(function(b,c,d){return b+(c?Math.pow(2,d):0)},0));
return a.h}
;function dk(){this.blockSize=-1}
;function ek(){this.blockSize=-1;this.blockSize=64;this.h=[];this.u=[];this.M=[];this.j=[];this.j[0]=128;for(var a=1;a<this.blockSize;++a)this.j[a]=0;this.o=this.i=0;this.reset()}
cb(ek,dk);ek.prototype.reset=function(){this.h[0]=1732584193;this.h[1]=4023233417;this.h[2]=2562383102;this.h[3]=271733878;this.h[4]=3285377520;this.o=this.i=0};
function fk(a,b,c){c||(c=0);var d=a.M;if(typeof b==="string")for(var e=0;e<16;e++)d[e]=b.charCodeAt(c)<<24|b.charCodeAt(c+1)<<16|b.charCodeAt(c+2)<<8|b.charCodeAt(c+3),c+=4;else for(e=0;e<16;e++)d[e]=b[c]<<24|b[c+1]<<16|b[c+2]<<8|b[c+3],c+=4;for(b=16;b<80;b++)c=d[b-3]^d[b-8]^d[b-14]^d[b-16],d[b]=(c<<1|c>>>31)&4294967295;b=a.h[0];c=a.h[1];e=a.h[2];for(var f=a.h[3],g=a.h[4],h,k,l=0;l<80;l++)l<40?l<20?(h=f^c&(e^f),k=1518500249):(h=c^e^f,k=1859775393):l<60?(h=c&e|f&(c|e),k=2400959708):(h=c^e^f,k=3395469782),
h=(b<<5|b>>>27)+h+g+k+d[l]&4294967295,g=f,f=e,e=(c<<30|c>>>2)&4294967295,c=b,b=h;a.h[0]=a.h[0]+b&4294967295;a.h[1]=a.h[1]+c&4294967295;a.h[2]=a.h[2]+e&4294967295;a.h[3]=a.h[3]+f&4294967295;a.h[4]=a.h[4]+g&4294967295}
ek.prototype.update=function(a,b){if(a!=null){b===void 0&&(b=a.length);for(var c=b-this.blockSize,d=0,e=this.u,f=this.i;d<b;){if(f==0)for(;d<=c;)fk(this,a,d),d+=this.blockSize;if(typeof a==="string")for(;d<b;){if(e[f]=a.charCodeAt(d),++f,++d,f==this.blockSize){fk(this,e);f=0;break}}else for(;d<b;)if(e[f]=a[d],++f,++d,f==this.blockSize){fk(this,e);f=0;break}}this.i=f;this.o+=b}};
ek.prototype.digest=function(){var a=[],b=this.o*8;this.i<56?this.update(this.j,56-this.i):this.update(this.j,this.blockSize-(this.i-56));for(var c=this.blockSize-1;c>=56;c--)this.u[c]=b&255,b/=256;fk(this,this.u);for(c=b=0;c<5;c++)for(var d=24;d>=0;d-=8)a[b]=this.h[c]>>d&255,++b;return a};function gk(a){return typeof a.className=="string"?a.className:a.getAttribute&&a.getAttribute("class")||""}
function hk(a,b){typeof a.className=="string"?a.className=b:a.setAttribute&&a.setAttribute("class",b)}
function ik(a,b){a.classList?b=a.classList.contains(b):(a=a.classList?a.classList:gk(a).match(/\S+/g)||[],b=Rb(a,b)>=0);return b}
function jk(){var a=document.body;a.classList?a.classList.remove("inverted-hdpi"):ik(a,"inverted-hdpi")&&hk(a,Array.prototype.filter.call(a.classList?a.classList:gk(a).match(/\S+/g)||[],function(b){return b!="inverted-hdpi"}).join(" "))}
;function kk(){}
kk.prototype.next=function(){return lk};
var lk={done:!0,value:void 0};kk.prototype.sb=function(){return this};function mk(a){if(a instanceof nk||a instanceof ok||a instanceof pk)return a;if(typeof a.next=="function")return new nk(function(){return a});
if(typeof a[Symbol.iterator]=="function")return new nk(function(){return a[Symbol.iterator]()});
if(typeof a.sb=="function")return new nk(function(){return a.sb()});
throw Error("Not an iterator or iterable.");}
function nk(a){this.h=a}
nk.prototype.sb=function(){return new ok(this.h())};
nk.prototype[Symbol.iterator]=function(){return new pk(this.h())};
nk.prototype.i=function(){return new pk(this.h())};
function ok(a){this.h=a}
w(ok,kk);ok.prototype.next=function(){return this.h.next()};
ok.prototype[Symbol.iterator]=function(){return new pk(this.h)};
ok.prototype.i=function(){return new pk(this.h)};
function pk(a){nk.call(this,function(){return a});
this.j=a}
w(pk,nk);pk.prototype.next=function(){return this.j.next()};function M(a){F.call(this);this.u=1;this.j=[];this.o=0;this.h=[];this.i={};this.D=!!a}
cb(M,F);r=M.prototype;r.subscribe=function(a,b,c){var d=this.i[a];d||(d=this.i[a]=[]);var e=this.u;this.h[e]=a;this.h[e+1]=b;this.h[e+2]=c;this.u=e+3;d.push(e);return e};
r.unsubscribe=function(a,b,c){if(a=this.i[a]){var d=this.h;if(a=a.find(function(e){return d[e+1]==b&&d[e+2]==c}))return this.ac(a)}return!1};
r.ac=function(a){var b=this.h[a];if(b){var c=this.i[b];this.o!=0?(this.j.push(a),this.h[a+1]=function(){}):(c&&Xb(c,a),delete this.h[a],delete this.h[a+1],delete this.h[a+2])}return!!b};
r.qb=function(a,b){var c=this.i[a];if(c){var d=Array(arguments.length-1),e=arguments.length,f;for(f=1;f<e;f++)d[f-1]=arguments[f];if(this.D)for(f=0;f<c.length;f++)e=c[f],qk(this.h[e+1],this.h[e+2],d);else{this.o++;try{for(f=0,e=c.length;f<e&&!this.ea;f++){var g=c[f];this.h[g+1].apply(this.h[g+2],d)}}finally{if(this.o--,this.j.length>0&&this.o==0)for(;c=this.j.pop();)this.ac(c)}}return f!=0}return!1};
function qk(a,b,c){pi(function(){a.apply(b,c)})}
r.clear=function(a){if(a){var b=this.i[a];b&&(b.forEach(this.ac,this),delete this.i[a])}else this.h.length=0,this.i={}};
r.ba=function(){M.Aa.ba.call(this);this.clear();this.j.length=0};function rk(a){this.h=a}
rk.prototype.set=function(a,b){b===void 0?this.h.remove(a):this.h.set(a,(new Ri).serialize(b))};
rk.prototype.get=function(a){try{var b=this.h.get(a)}catch(c){return}if(b!==null)try{return JSON.parse(b)}catch(c){throw"Storage: Invalid value was encountered";}};
rk.prototype.remove=function(a){this.h.remove(a)};function sk(a){this.h=a}
cb(sk,rk);function tk(a){this.data=a}
function uk(a){return a===void 0||a instanceof tk?a:new tk(a)}
sk.prototype.set=function(a,b){sk.Aa.set.call(this,a,uk(b))};
sk.prototype.i=function(a){a=sk.Aa.get.call(this,a);if(a===void 0||a instanceof Object)return a;throw"Storage: Invalid value was encountered";};
sk.prototype.get=function(a){if(a=this.i(a)){if(a=a.data,a===void 0)throw"Storage: Invalid value was encountered";}else a=void 0;return a};function vk(a){this.h=a}
cb(vk,sk);vk.prototype.set=function(a,b,c){if(b=uk(b)){if(c){if(c<ab()){vk.prototype.remove.call(this,a);return}b.expiration=c}b.creation=ab()}vk.Aa.set.call(this,a,b)};
vk.prototype.i=function(a){var b=vk.Aa.i.call(this,a);if(b){var c=b.creation,d=b.expiration;if(d&&d<ab()||c&&c>ab())vk.prototype.remove.call(this,a);else return b}};function wk(){}
;function xk(){}
cb(xk,wk);xk.prototype[Symbol.iterator]=function(){return mk(this.sb(!0)).i()};
xk.prototype.clear=function(){var a=Array.from(this);a=y(a);for(var b=a.next();!b.done;b=a.next())this.remove(b.value)};function yk(a){this.h=a;this.i=null}
cb(yk,xk);r=yk.prototype;r.isAvailable=function(){var a=this.h;if(a)try{a.setItem("__sak","1");a.removeItem("__sak");var b=!0}catch(c){b=c instanceof DOMException&&(c.name==="QuotaExceededError"||c.code===22||c.code===1014||c.name==="NS_ERROR_DOM_QUOTA_REACHED")&&a&&a.length!==0}else b=!1;return this.i=b};
r.set=function(a,b){zk(this);try{this.h.setItem(a,b)}catch(c){if(this.h.length==0)throw"Storage mechanism: Storage disabled";throw"Storage mechanism: Quota exceeded";}};
r.get=function(a){zk(this);a=this.h.getItem(a);if(typeof a!=="string"&&a!==null)throw"Storage mechanism: Invalid value was encountered";return a};
r.remove=function(a){zk(this);this.h.removeItem(a)};
r.sb=function(a){zk(this);var b=0,c=this.h,d=new kk;d.next=function(){if(b>=c.length)return lk;var e=c.key(b++);if(a)return{value:e,done:!1};e=c.getItem(e);if(typeof e!=="string")throw"Storage mechanism: Invalid value was encountered";return{value:e,done:!1}};
return d};
r.clear=function(){zk(this);this.h.clear()};
r.key=function(a){zk(this);return this.h.key(a)};
function zk(a){if(a.h==null)throw Error("Storage mechanism: Storage unavailable");var b;((b=a.i)!=null?b:a.isAvailable())||Oc(Error("Storage mechanism: Storage unavailable"))}
;function Ak(){var a=null;try{a=C.localStorage||null}catch(b){}yk.call(this,a)}
cb(Ak,yk);function Bk(a,b){this.i=a;this.h=b+"::"}
cb(Bk,xk);Bk.prototype.set=function(a,b){this.i.set(this.h+a,b)};
Bk.prototype.get=function(a){return this.i.get(this.h+a)};
Bk.prototype.remove=function(a){this.i.remove(this.h+a)};
Bk.prototype.sb=function(a){var b=this.i[Symbol.iterator](),c=this,d=new kk;d.next=function(){var e=b.next();if(e.done)return e;for(e=e.value;e.slice(0,c.h.length)!=c.h;){e=b.next();if(e.done)return e;e=e.value}return{value:a?e.slice(c.h.length):c.i.get(e),done:!1}};
return d};/*

 (The MIT License)

 Copyright (C) 2014 by Vitaly Puzrin

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 THE SOFTWARE.

 -----------------------------------------------------------------------------
 Ported from zlib, which is under the following license
 https://github.com/madler/zlib/blob/master/zlib.h

 zlib.h -- interface of the 'zlib' general purpose compression library
   version 1.2.8, April 28th, 2013
   Copyright (C) 1995-2013 Jean-loup Gailly and Mark Adler
   This software is provided 'as-is', without any express or implied
   warranty.  In no event will the authors be held liable for any damages
   arising from the use of this software.
   Permission is granted to anyone to use this software for any purpose,
   including commercial applications, and to alter it and redistribute it
   freely, subject to the following restrictions:
   1. The origin of this software must not be misrepresented; you must not
      claim that you wrote the original software. If you use this software
      in a product, an acknowledgment in the product documentation would be
      appreciated but is not required.
   2. Altered source versions must be plainly marked as such, and must not be
      misrepresented as being the original software.
   3. This notice may not be removed or altered from any source distribution.
   Jean-loup Gailly        Mark Adler
   jloup@gzip.org          madler@alumni.caltech.edu
   The data format used by the zlib library is described by RFCs (Request for
   Comments) 1950 to 1952 in the files http://tools.ietf.org/html/rfc1950
   (zlib format), rfc1951 (deflate format) and rfc1952 (gzip format).
*/
var N={},Ck=typeof Uint8Array!=="undefined"&&typeof Uint16Array!=="undefined"&&typeof Int32Array!=="undefined";N.assign=function(a){for(var b=Array.prototype.slice.call(arguments,1);b.length;){var c=b.shift();if(c){if(typeof c!=="object")throw new TypeError(c+"must be non-object");for(var d in c)Object.prototype.hasOwnProperty.call(c,d)&&(a[d]=c[d])}}return a};
N.cd=function(a,b){if(a.length===b)return a;if(a.subarray)return a.subarray(0,b);a.length=b;return a};
var Dk={tb:function(a,b,c,d,e){if(b.subarray&&a.subarray)a.set(b.subarray(c,c+d),e);else for(var f=0;f<d;f++)a[e+f]=b[c+f]},
td:function(a){var b,c;var d=c=0;for(b=a.length;d<b;d++)c+=a[d].length;var e=new Uint8Array(c);d=c=0;for(b=a.length;d<b;d++){var f=a[d];e.set(f,c);c+=f.length}return e}},Ek={tb:function(a,b,c,d,e){for(var f=0;f<d;f++)a[e+f]=b[c+f]},
td:function(a){return[].concat.apply([],a)}};
N.qf=function(){Ck?(N.pb=Uint8Array,N.Ja=Uint16Array,N.Vd=Int32Array,N.assign(N,Dk)):(N.pb=Array,N.Ja=Array,N.Vd=Array,N.assign(N,Ek))};
N.qf();var Fk=!0;try{new Uint8Array(1)}catch(a){Fk=!1}
function Gk(a){var b,c,d=a.length,e=0;for(b=0;b<d;b++){var f=a.charCodeAt(b);if((f&64512)===55296&&b+1<d){var g=a.charCodeAt(b+1);(g&64512)===56320&&(f=65536+(f-55296<<10)+(g-56320),b++)}e+=f<128?1:f<2048?2:f<65536?3:4}var h=new N.pb(e);for(b=c=0;c<e;b++)f=a.charCodeAt(b),(f&64512)===55296&&b+1<d&&(g=a.charCodeAt(b+1),(g&64512)===56320&&(f=65536+(f-55296<<10)+(g-56320),b++)),f<128?h[c++]=f:(f<2048?h[c++]=192|f>>>6:(f<65536?h[c++]=224|f>>>12:(h[c++]=240|f>>>18,h[c++]=128|f>>>12&63),h[c++]=128|f>>>
6&63),h[c++]=128|f&63);return h}
;var Hk={};Hk=function(a,b,c,d){var e=a&65535|0;a=a>>>16&65535|0;for(var f;c!==0;){f=c>2E3?2E3:c;c-=f;do e=e+b[d++]|0,a=a+e|0;while(--f);e%=65521;a%=65521}return e|a<<16|0};for(var Ik={},Jk,Kk=[],Lk=0;Lk<256;Lk++){Jk=Lk;for(var Mk=0;Mk<8;Mk++)Jk=Jk&1?3988292384^Jk>>>1:Jk>>>1;Kk[Lk]=Jk}Ik=function(a,b,c,d){c=d+c;for(a^=-1;d<c;d++)a=a>>>8^Kk[(a^b[d])&255];return a^-1};var Nk={};Nk={2:"need dictionary",1:"stream end",0:"","-1":"file error","-2":"stream error","-3":"data error","-4":"insufficient memory","-5":"buffer error","-6":"incompatible version"};function Ok(a){for(var b=a.length;--b>=0;)a[b]=0}
var Pk=[0,0,0,0,0,0,0,0,1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5,0],Qk=[0,0,0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13],Rk=[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,7],Sk=[16,17,18,0,8,7,9,6,10,5,11,4,12,3,13,2,14,1,15],Tk=Array(576);Ok(Tk);var Uk=Array(60);Ok(Uk);var Vk=Array(512);Ok(Vk);var Wk=Array(256);Ok(Wk);var Xk=Array(29);Ok(Xk);var Yk=Array(30);Ok(Yk);function Zk(a,b,c,d,e){this.Ld=a;this.ue=b;this.te=c;this.ne=d;this.Me=e;this.wd=a&&a.length}
var $k,al,bl;function cl(a,b){this.sd=a;this.Db=0;this.bb=b}
function dl(a,b){a.aa[a.pending++]=b&255;a.aa[a.pending++]=b>>>8&255}
function el(a,b,c){a.ia>16-c?(a.oa|=b<<a.ia&65535,dl(a,a.oa),a.oa=b>>16-a.ia,a.ia+=c-16):(a.oa|=b<<a.ia&65535,a.ia+=c)}
function fl(a,b,c){el(a,c[b*2],c[b*2+1])}
function gl(a,b){var c=0;do c|=a&1,a>>>=1,c<<=1;while(--b>0);return c>>>1}
function hl(a,b,c){var d=Array(16),e=0,f;for(f=1;f<=15;f++)d[f]=e=e+c[f-1]<<1;for(c=0;c<=b;c++)e=a[c*2+1],e!==0&&(a[c*2]=gl(d[e]++,e))}
function il(a){var b;for(b=0;b<286;b++)a.ra[b*2]=0;for(b=0;b<30;b++)a.fb[b*2]=0;for(b=0;b<19;b++)a.ja[b*2]=0;a.ra[512]=1;a.Oa=a.Gb=0;a.ya=a.matches=0}
function jl(a){a.ia>8?dl(a,a.oa):a.ia>0&&(a.aa[a.pending++]=a.oa);a.oa=0;a.ia=0}
function kl(a,b,c){jl(a);dl(a,c);dl(a,~c);N.tb(a.aa,a.window,b,c,a.pending);a.pending+=c}
function ll(a,b,c,d){var e=b*2,f=c*2;return a[e]<a[f]||a[e]===a[f]&&d[b]<=d[c]}
function ml(a,b,c){for(var d=a.da[c],e=c<<1;e<=a.Na;){e<a.Na&&ll(b,a.da[e+1],a.da[e],a.depth)&&e++;if(ll(b,d,a.da[e],a.depth))break;a.da[c]=a.da[e];c=e;e<<=1}a.da[c]=d}
function nl(a,b,c){var d=0;if(a.ya!==0){do{var e=a.aa[a.Nb+d*2]<<8|a.aa[a.Nb+d*2+1];var f=a.aa[a.Oc+d];d++;if(e===0)fl(a,f,b);else{var g=Wk[f];fl(a,g+256+1,b);var h=Pk[g];h!==0&&(f-=Xk[g],el(a,f,h));e--;g=e<256?Vk[e]:Vk[256+(e>>>7)];fl(a,g,c);h=Qk[g];h!==0&&(e-=Yk[g],el(a,e,h))}}while(d<a.ya)}fl(a,256,b)}
function ol(a,b){var c=b.sd,d=b.bb.Ld,e=b.bb.wd,f=b.bb.ne,g,h=-1;a.Na=0;a.yb=573;for(g=0;g<f;g++)c[g*2]!==0?(a.da[++a.Na]=h=g,a.depth[g]=0):c[g*2+1]=0;for(;a.Na<2;){var k=a.da[++a.Na]=h<2?++h:0;c[k*2]=1;a.depth[k]=0;a.Oa--;e&&(a.Gb-=d[k*2+1])}b.Db=h;for(g=a.Na>>1;g>=1;g--)ml(a,c,g);k=f;do g=a.da[1],a.da[1]=a.da[a.Na--],ml(a,c,1),d=a.da[1],a.da[--a.yb]=g,a.da[--a.yb]=d,c[k*2]=c[g*2]+c[d*2],a.depth[k]=(a.depth[g]>=a.depth[d]?a.depth[g]:a.depth[d])+1,c[g*2+1]=c[d*2+1]=k,a.da[1]=k++,ml(a,c,1);while(a.Na>=
2);a.da[--a.yb]=a.da[1];g=b.sd;k=b.Db;d=b.bb.Ld;e=b.bb.wd;f=b.bb.ue;var l=b.bb.te,m=b.bb.Me,n,p=0;for(n=0;n<=15;n++)a.Ka[n]=0;g[a.da[a.yb]*2+1]=0;for(b=a.yb+1;b<573;b++){var t=a.da[b];n=g[g[t*2+1]*2+1]+1;n>m&&(n=m,p++);g[t*2+1]=n;if(!(t>k)){a.Ka[n]++;var u=0;t>=l&&(u=f[t-l]);var x=g[t*2];a.Oa+=x*(n+u);e&&(a.Gb+=x*(d[t*2+1]+u))}}if(p!==0){do{for(n=m-1;a.Ka[n]===0;)n--;a.Ka[n]--;a.Ka[n+1]+=2;a.Ka[m]--;p-=2}while(p>0);for(n=m;n!==0;n--)for(t=a.Ka[n];t!==0;)d=a.da[--b],d>k||(g[d*2+1]!==n&&(a.Oa+=(n-g[d*
2+1])*g[d*2],g[d*2+1]=n),t--)}hl(c,h,a.Ka)}
function pl(a,b,c){var d,e=-1,f=b[1],g=0,h=7,k=4;f===0&&(h=138,k=3);b[(c+1)*2+1]=65535;for(d=0;d<=c;d++){var l=f;f=b[(d+1)*2+1];++g<h&&l===f||(g<k?a.ja[l*2]+=g:l!==0?(l!==e&&a.ja[l*2]++,a.ja[32]++):g<=10?a.ja[34]++:a.ja[36]++,g=0,e=l,f===0?(h=138,k=3):l===f?(h=6,k=3):(h=7,k=4))}}
function ql(a,b,c){var d,e=-1,f=b[1],g=0,h=7,k=4;f===0&&(h=138,k=3);for(d=0;d<=c;d++){var l=f;f=b[(d+1)*2+1];if(!(++g<h&&l===f)){if(g<k){do fl(a,l,a.ja);while(--g!==0)}else l!==0?(l!==e&&(fl(a,l,a.ja),g--),fl(a,16,a.ja),el(a,g-3,2)):g<=10?(fl(a,17,a.ja),el(a,g-3,3)):(fl(a,18,a.ja),el(a,g-11,7));g=0;e=l;f===0?(h=138,k=3):l===f?(h=6,k=3):(h=7,k=4)}}}
function rl(a){var b=4093624447,c;for(c=0;c<=31;c++,b>>>=1)if(b&1&&a.ra[c*2]!==0)return 0;if(a.ra[18]!==0||a.ra[20]!==0||a.ra[26]!==0)return 1;for(c=32;c<256;c++)if(a.ra[c*2]!==0)return 1;return 0}
var sl=!1;function tl(a,b,c){a.aa[a.Nb+a.ya*2]=b>>>8&255;a.aa[a.Nb+a.ya*2+1]=b&255;a.aa[a.Oc+a.ya]=c&255;a.ya++;b===0?a.ra[c*2]++:(a.matches++,b--,a.ra[(Wk[c]+256+1)*2]++,a.fb[(b<256?Vk[b]:Vk[256+(b>>>7)])*2]++);return a.ya===a.Rb-1}
;function ul(a,b){a.msg=Nk[b];return b}
function vl(a){for(var b=a.length;--b>=0;)a[b]=0}
function wl(a){var b=a.state,c=b.pending;c>a.S&&(c=a.S);c!==0&&(N.tb(a.output,b.aa,b.Ub,c,a.Eb),a.Eb+=c,b.Ub+=c,a.hd+=c,a.S-=c,b.pending-=c,b.pending===0&&(b.Ub=0))}
function xl(a,b){var c=a.va>=0?a.va:-1,d=a.v-a.va,e=0;if(a.level>0){a.K.Hc===2&&(a.K.Hc=rl(a));ol(a,a.qc);ol(a,a.ic);pl(a,a.ra,a.qc.Db);pl(a,a.fb,a.ic.Db);ol(a,a.od);for(e=18;e>=3&&a.ja[Sk[e]*2+1]===0;e--);a.Oa+=3*(e+1)+5+5+4;var f=a.Oa+3+7>>>3;var g=a.Gb+3+7>>>3;g<=f&&(f=g)}else f=g=d+5;if(d+4<=f&&c!==-1)el(a,b?1:0,3),kl(a,c,d);else if(a.strategy===4||g===f)el(a,2+(b?1:0),3),nl(a,Tk,Uk);else{el(a,4+(b?1:0),3);c=a.qc.Db+1;d=a.ic.Db+1;e+=1;el(a,c-257,5);el(a,d-1,5);el(a,e-4,4);for(f=0;f<e;f++)el(a,
a.ja[Sk[f]*2+1],3);ql(a,a.ra,c-1);ql(a,a.fb,d-1);nl(a,a.ra,a.fb)}il(a);b&&jl(a);a.va=a.v;wl(a.K)}
function O(a,b){a.aa[a.pending++]=b}
function yl(a,b){a.aa[a.pending++]=b>>>8&255;a.aa[a.pending++]=b&255}
function zl(a,b){var c=a.zd,d=a.v,e=a.wa,f=a.Bd,g=a.v>a.la-262?a.v-(a.la-262):0,h=a.window,k=a.cb,l=a.Ia,m=a.v+258,n=h[d+e-1],p=h[d+e];a.wa>=a.vd&&(c>>=2);f>a.B&&(f=a.B);do{var t=b;if(h[t+e]===p&&h[t+e-1]===n&&h[t]===h[d]&&h[++t]===h[d+1]){d+=2;for(t++;h[++d]===h[++t]&&h[++d]===h[++t]&&h[++d]===h[++t]&&h[++d]===h[++t]&&h[++d]===h[++t]&&h[++d]===h[++t]&&h[++d]===h[++t]&&h[++d]===h[++t]&&d<m;);t=258-(m-d);d=m-258;if(t>e){a.Cb=b;e=t;if(t>=f)break;n=h[d+e-1];p=h[d+e]}}}while((b=l[b&k])>g&&--c!==0);return e<=
a.B?e:a.B}
function Al(a){var b=a.la,c;do{var d=a.Td-a.B-a.v;if(a.v>=b+(b-262)){N.tb(a.window,a.window,b,b,0);a.Cb-=b;a.v-=b;a.va-=b;var e=c=a.pc;do{var f=a.head[--e];a.head[e]=f>=b?f-b:0}while(--c);e=c=b;do f=a.Ia[--e],a.Ia[e]=f>=b?f-b:0;while(--c);d+=b}if(a.K.na===0)break;e=a.K;c=a.window;f=a.v+a.B;var g=e.na;g>d&&(g=d);g===0?c=0:(e.na-=g,N.tb(c,e.input,e.lb,g,f),e.state.wrap===1?e.I=Hk(e.I,c,g,f):e.state.wrap===2&&(e.I=Ik(e.I,c,g,f)),e.lb+=g,e.nb+=g,c=g);a.B+=c;if(a.B+a.sa>=3)for(d=a.v-a.sa,a.R=a.window[d],
a.R=(a.R<<a.Ma^a.window[d+1])&a.La;a.sa&&!(a.R=(a.R<<a.Ma^a.window[d+3-1])&a.La,a.Ia[d&a.cb]=a.head[a.R],a.head[a.R]=d,d++,a.sa--,a.B+a.sa<3););}while(a.B<262&&a.K.na!==0)}
function Bl(a,b){for(var c;;){if(a.B<262){Al(a);if(a.B<262&&b===0)return 1;if(a.B===0)break}c=0;a.B>=3&&(a.R=(a.R<<a.Ma^a.window[a.v+3-1])&a.La,c=a.Ia[a.v&a.cb]=a.head[a.R],a.head[a.R]=a.v);c!==0&&a.v-c<=a.la-262&&(a.T=zl(a,c));if(a.T>=3)if(c=tl(a,a.v-a.Cb,a.T-3),a.B-=a.T,a.T<=a.Qc&&a.B>=3){a.T--;do a.v++,a.R=(a.R<<a.Ma^a.window[a.v+3-1])&a.La,a.Ia[a.v&a.cb]=a.head[a.R],a.head[a.R]=a.v;while(--a.T!==0);a.v++}else a.v+=a.T,a.T=0,a.R=a.window[a.v],a.R=(a.R<<a.Ma^a.window[a.v+1])&a.La;else c=tl(a,0,
a.window[a.v]),a.B--,a.v++;if(c&&(xl(a,!1),a.K.S===0))return 1}a.sa=a.v<2?a.v:2;return b===4?(xl(a,!0),a.K.S===0?3:4):a.ya&&(xl(a,!1),a.K.S===0)?1:2}
function Cl(a,b){for(var c,d;;){if(a.B<262){Al(a);if(a.B<262&&b===0)return 1;if(a.B===0)break}c=0;a.B>=3&&(a.R=(a.R<<a.Ma^a.window[a.v+3-1])&a.La,c=a.Ia[a.v&a.cb]=a.head[a.R],a.head[a.R]=a.v);a.wa=a.T;a.Ed=a.Cb;a.T=2;c!==0&&a.wa<a.Qc&&a.v-c<=a.la-262&&(a.T=zl(a,c),a.T<=5&&(a.strategy===1||a.T===3&&a.v-a.Cb>4096)&&(a.T=2));if(a.wa>=3&&a.T<=a.wa){d=a.v+a.B-3;c=tl(a,a.v-1-a.Ed,a.wa-3);a.B-=a.wa-1;a.wa-=2;do++a.v<=d&&(a.R=(a.R<<a.Ma^a.window[a.v+3-1])&a.La,a.Ia[a.v&a.cb]=a.head[a.R],a.head[a.R]=a.v);
while(--a.wa!==0);a.jb=0;a.T=2;a.v++;if(c&&(xl(a,!1),a.K.S===0))return 1}else if(a.jb){if((c=tl(a,0,a.window[a.v-1]))&&xl(a,!1),a.v++,a.B--,a.K.S===0)return 1}else a.jb=1,a.v++,a.B--}a.jb&&(tl(a,0,a.window[a.v-1]),a.jb=0);a.sa=a.v<2?a.v:2;return b===4?(xl(a,!0),a.K.S===0?3:4):a.ya&&(xl(a,!1),a.K.S===0)?1:2}
function Dl(a,b){for(var c,d,e,f=a.window;;){if(a.B<=258){Al(a);if(a.B<=258&&b===0)return 1;if(a.B===0)break}a.T=0;if(a.B>=3&&a.v>0&&(d=a.v-1,c=f[d],c===f[++d]&&c===f[++d]&&c===f[++d])){for(e=a.v+258;c===f[++d]&&c===f[++d]&&c===f[++d]&&c===f[++d]&&c===f[++d]&&c===f[++d]&&c===f[++d]&&c===f[++d]&&d<e;);a.T=258-(e-d);a.T>a.B&&(a.T=a.B)}a.T>=3?(c=tl(a,1,a.T-3),a.B-=a.T,a.v+=a.T,a.T=0):(c=tl(a,0,a.window[a.v]),a.B--,a.v++);if(c&&(xl(a,!1),a.K.S===0))return 1}a.sa=0;return b===4?(xl(a,!0),a.K.S===0?3:4):
a.ya&&(xl(a,!1),a.K.S===0)?1:2}
function El(a,b){for(var c;;){if(a.B===0&&(Al(a),a.B===0)){if(b===0)return 1;break}a.T=0;c=tl(a,0,a.window[a.v]);a.B--;a.v++;if(c&&(xl(a,!1),a.K.S===0))return 1}a.sa=0;return b===4?(xl(a,!0),a.K.S===0?3:4):a.ya&&(xl(a,!1),a.K.S===0)?1:2}
function Fl(a,b,c,d,e){this.ze=a;this.Le=b;this.Ne=c;this.Ke=d;this.we=e}
var Gl;Gl=[new Fl(0,0,0,0,function(a,b){var c=65535;for(c>a.za-5&&(c=a.za-5);;){if(a.B<=1){Al(a);if(a.B===0&&b===0)return 1;if(a.B===0)break}a.v+=a.B;a.B=0;var d=a.va+c;if(a.v===0||a.v>=d)if(a.B=a.v-d,a.v=d,xl(a,!1),a.K.S===0)return 1;if(a.v-a.va>=a.la-262&&(xl(a,!1),a.K.S===0))return 1}a.sa=0;if(b===4)return xl(a,!0),a.K.S===0?3:4;a.v>a.va&&xl(a,!1);return 1}),
new Fl(4,4,8,4,Bl),new Fl(4,5,16,8,Bl),new Fl(4,6,32,32,Bl),new Fl(4,4,16,16,Cl),new Fl(8,16,32,32,Cl),new Fl(8,16,128,128,Cl),new Fl(8,32,128,256,Cl),new Fl(32,128,258,1024,Cl),new Fl(32,258,258,4096,Cl)];
function Hl(){this.K=null;this.status=0;this.aa=null;this.wrap=this.pending=this.Ub=this.za=0;this.H=null;this.Ba=0;this.method=8;this.Ab=-1;this.cb=this.kd=this.la=0;this.window=null;this.Td=0;this.head=this.Ia=null;this.Bd=this.vd=this.strategy=this.level=this.Qc=this.zd=this.wa=this.B=this.Cb=this.v=this.jb=this.Ed=this.T=this.va=this.Ma=this.La=this.Mc=this.pc=this.R=0;this.ra=new N.Ja(1146);this.fb=new N.Ja(122);this.ja=new N.Ja(78);vl(this.ra);vl(this.fb);vl(this.ja);this.od=this.ic=this.qc=
null;this.Ka=new N.Ja(16);this.da=new N.Ja(573);vl(this.da);this.yb=this.Na=0;this.depth=new N.Ja(573);vl(this.depth);this.ia=this.oa=this.sa=this.matches=this.Gb=this.Oa=this.Nb=this.ya=this.Rb=this.Oc=0}
function Il(a,b){if(!a||!a.state||b>5||b<0)return a?ul(a,-2):-2;var c=a.state;if(!a.output||!a.input&&a.na!==0||c.status===666&&b!==4)return ul(a,a.S===0?-5:-2);c.K=a;var d=c.Ab;c.Ab=b;if(c.status===42)if(c.wrap===2)a.I=0,O(c,31),O(c,139),O(c,8),c.H?(O(c,(c.H.text?1:0)+(c.H.Va?2:0)+(c.H.extra?4:0)+(c.H.name?8:0)+(c.H.comment?16:0)),O(c,c.H.time&255),O(c,c.H.time>>8&255),O(c,c.H.time>>16&255),O(c,c.H.time>>24&255),O(c,c.level===9?2:c.strategy>=2||c.level<2?4:0),O(c,c.H.os&255),c.H.extra&&c.H.extra.length&&
(O(c,c.H.extra.length&255),O(c,c.H.extra.length>>8&255)),c.H.Va&&(a.I=Ik(a.I,c.aa,c.pending,0)),c.Ba=0,c.status=69):(O(c,0),O(c,0),O(c,0),O(c,0),O(c,0),O(c,c.level===9?2:c.strategy>=2||c.level<2?4:0),O(c,3),c.status=113);else{var e=8+(c.kd-8<<4)<<8;e|=(c.strategy>=2||c.level<2?0:c.level<6?1:c.level===6?2:3)<<6;c.v!==0&&(e|=32);c.status=113;yl(c,e+(31-e%31));c.v!==0&&(yl(c,a.I>>>16),yl(c,a.I&65535));a.I=1}if(c.status===69)if(c.H.extra){for(e=c.pending;c.Ba<(c.H.extra.length&65535)&&(c.pending!==c.za||
(c.H.Va&&c.pending>e&&(a.I=Ik(a.I,c.aa,c.pending-e,e)),wl(a),e=c.pending,c.pending!==c.za));)O(c,c.H.extra[c.Ba]&255),c.Ba++;c.H.Va&&c.pending>e&&(a.I=Ik(a.I,c.aa,c.pending-e,e));c.Ba===c.H.extra.length&&(c.Ba=0,c.status=73)}else c.status=73;if(c.status===73)if(c.H.name){e=c.pending;do{if(c.pending===c.za&&(c.H.Va&&c.pending>e&&(a.I=Ik(a.I,c.aa,c.pending-e,e)),wl(a),e=c.pending,c.pending===c.za)){var f=1;break}f=c.Ba<c.H.name.length?c.H.name.charCodeAt(c.Ba++)&255:0;O(c,f)}while(f!==0);c.H.Va&&c.pending>
e&&(a.I=Ik(a.I,c.aa,c.pending-e,e));f===0&&(c.Ba=0,c.status=91)}else c.status=91;if(c.status===91)if(c.H.comment){e=c.pending;do{if(c.pending===c.za&&(c.H.Va&&c.pending>e&&(a.I=Ik(a.I,c.aa,c.pending-e,e)),wl(a),e=c.pending,c.pending===c.za)){f=1;break}f=c.Ba<c.H.comment.length?c.H.comment.charCodeAt(c.Ba++)&255:0;O(c,f)}while(f!==0);c.H.Va&&c.pending>e&&(a.I=Ik(a.I,c.aa,c.pending-e,e));f===0&&(c.status=103)}else c.status=103;c.status===103&&(c.H.Va?(c.pending+2>c.za&&wl(a),c.pending+2<=c.za&&(O(c,
a.I&255),O(c,a.I>>8&255),a.I=0,c.status=113)):c.status=113);if(c.pending!==0){if(wl(a),a.S===0)return c.Ab=-1,0}else if(a.na===0&&(b<<1)-(b>4?9:0)<=(d<<1)-(d>4?9:0)&&b!==4)return ul(a,-5);if(c.status===666&&a.na!==0)return ul(a,-5);if(a.na!==0||c.B!==0||b!==0&&c.status!==666){d=c.strategy===2?El(c,b):c.strategy===3?Dl(c,b):Gl[c.level].we(c,b);if(d===3||d===4)c.status=666;if(d===1||d===3)return a.S===0&&(c.Ab=-1),0;if(d===2&&(b===1?(el(c,2,3),fl(c,256,Tk),c.ia===16?(dl(c,c.oa),c.oa=0,c.ia=0):c.ia>=
8&&(c.aa[c.pending++]=c.oa&255,c.oa>>=8,c.ia-=8)):b!==5&&(el(c,0,3),kl(c,0,0),b===3&&(vl(c.head),c.B===0&&(c.v=0,c.va=0,c.sa=0))),wl(a),a.S===0))return c.Ab=-1,0}if(b!==4)return 0;if(c.wrap<=0)return 1;c.wrap===2?(O(c,a.I&255),O(c,a.I>>8&255),O(c,a.I>>16&255),O(c,a.I>>24&255),O(c,a.nb&255),O(c,a.nb>>8&255),O(c,a.nb>>16&255),O(c,a.nb>>24&255)):(yl(c,a.I>>>16),yl(c,a.I&65535));wl(a);c.wrap>0&&(c.wrap=-c.wrap);return c.pending!==0?0:1}
;var Jl={};Jl=function(){this.input=null;this.nb=this.na=this.lb=0;this.output=null;this.hd=this.S=this.Eb=0;this.msg="";this.state=null;this.Hc=2;this.I=0};var Kl=Object.prototype.toString;
function Ll(a){if(!(this instanceof Ll))return new Ll(a);a=this.options=N.assign({level:-1,method:8,chunkSize:16384,windowBits:15,memLevel:8,strategy:0,to:""},a||{});a.raw&&a.windowBits>0?a.windowBits=-a.windowBits:a.gzip&&a.windowBits>0&&a.windowBits<16&&(a.windowBits+=16);this.err=0;this.msg="";this.ended=!1;this.chunks=[];this.K=new Jl;this.K.S=0;var b=this.K;var c=a.level,d=a.method,e=a.windowBits,f=a.memLevel,g=a.strategy;if(b){var h=1;c===-1&&(c=6);e<0?(h=0,e=-e):e>15&&(h=2,e-=16);if(f<1||f>
9||d!==8||e<8||e>15||c<0||c>9||g<0||g>4)b=ul(b,-2);else{e===8&&(e=9);var k=new Hl;b.state=k;k.K=b;k.wrap=h;k.H=null;k.kd=e;k.la=1<<k.kd;k.cb=k.la-1;k.Mc=f+7;k.pc=1<<k.Mc;k.La=k.pc-1;k.Ma=~~((k.Mc+3-1)/3);k.window=new N.pb(k.la*2);k.head=new N.Ja(k.pc);k.Ia=new N.Ja(k.la);k.Rb=1<<f+6;k.za=k.Rb*4;k.aa=new N.pb(k.za);k.Nb=1*k.Rb;k.Oc=3*k.Rb;k.level=c;k.strategy=g;k.method=d;if(b&&b.state){b.nb=b.hd=0;b.Hc=2;c=b.state;c.pending=0;c.Ub=0;c.wrap<0&&(c.wrap=-c.wrap);c.status=c.wrap?42:113;b.I=c.wrap===2?
0:1;c.Ab=0;if(!sl){d=Array(16);for(f=g=0;f<28;f++)for(Xk[f]=g,e=0;e<1<<Pk[f];e++)Wk[g++]=f;Wk[g-1]=f;for(f=g=0;f<16;f++)for(Yk[f]=g,e=0;e<1<<Qk[f];e++)Vk[g++]=f;for(g>>=7;f<30;f++)for(Yk[f]=g<<7,e=0;e<1<<Qk[f]-7;e++)Vk[256+g++]=f;for(e=0;e<=15;e++)d[e]=0;for(e=0;e<=143;)Tk[e*2+1]=8,e++,d[8]++;for(;e<=255;)Tk[e*2+1]=9,e++,d[9]++;for(;e<=279;)Tk[e*2+1]=7,e++,d[7]++;for(;e<=287;)Tk[e*2+1]=8,e++,d[8]++;hl(Tk,287,d);for(e=0;e<30;e++)Uk[e*2+1]=5,Uk[e*2]=gl(e,5);$k=new Zk(Tk,Pk,257,286,15);al=new Zk(Uk,
Qk,0,30,15);bl=new Zk([],Rk,0,19,7);sl=!0}c.qc=new cl(c.ra,$k);c.ic=new cl(c.fb,al);c.od=new cl(c.ja,bl);c.oa=0;c.ia=0;il(c);c=0}else c=ul(b,-2);c===0&&(b=b.state,b.Td=2*b.la,vl(b.head),b.Qc=Gl[b.level].Le,b.vd=Gl[b.level].ze,b.Bd=Gl[b.level].Ne,b.zd=Gl[b.level].Ke,b.v=0,b.va=0,b.B=0,b.sa=0,b.T=b.wa=2,b.jb=0,b.R=0);b=c}}else b=-2;if(b!==0)throw Error(Nk[b]);a.header&&(b=this.K)&&b.state&&b.state.wrap===2&&(b.state.H=a.header);if(a.dictionary){var l;typeof a.dictionary==="string"?l=Gk(a.dictionary):
Kl.call(a.dictionary)==="[object ArrayBuffer]"?l=new Uint8Array(a.dictionary):l=a.dictionary;a=this.K;f=l;g=f.length;if(a&&a.state)if(l=a.state,b=l.wrap,b===2||b===1&&l.status!==42||l.B)b=-2;else{b===1&&(a.I=Hk(a.I,f,g,0));l.wrap=0;g>=l.la&&(b===0&&(vl(l.head),l.v=0,l.va=0,l.sa=0),c=new N.pb(l.la),N.tb(c,f,g-l.la,l.la,0),f=c,g=l.la);c=a.na;d=a.lb;e=a.input;a.na=g;a.lb=0;a.input=f;for(Al(l);l.B>=3;){f=l.v;g=l.B-2;do l.R=(l.R<<l.Ma^l.window[f+3-1])&l.La,l.Ia[f&l.cb]=l.head[l.R],l.head[l.R]=f,f++;while(--g);
l.v=f;l.B=2;Al(l)}l.v+=l.B;l.va=l.v;l.sa=l.B;l.B=0;l.T=l.wa=2;l.jb=0;a.lb=d;a.input=e;a.na=c;l.wrap=b;b=0}else b=-2;if(b!==0)throw Error(Nk[b]);this.sh=!0}}
Ll.prototype.push=function(a,b){var c=this.K,d=this.options.chunkSize;if(this.ended)return!1;var e=b===~~b?b:b===!0?4:0;typeof a==="string"?c.input=Gk(a):Kl.call(a)==="[object ArrayBuffer]"?c.input=new Uint8Array(a):c.input=a;c.lb=0;c.na=c.input.length;do{c.S===0&&(c.output=new N.pb(d),c.Eb=0,c.S=d);a=Il(c,e);if(a!==1&&a!==0)return Ml(this,a),this.ended=!0,!1;if(c.S===0||c.na===0&&(e===4||e===2))if(this.options.to==="string"){var f=N.cd(c.output,c.Eb);b=f;f=f.length;if(f<65537&&(b.subarray&&Fk||!b.subarray))b=
String.fromCharCode.apply(null,N.cd(b,f));else{for(var g="",h=0;h<f;h++)g+=String.fromCharCode(b[h]);b=g}this.chunks.push(b)}else b=N.cd(c.output,c.Eb),this.chunks.push(b)}while((c.na>0||c.S===0)&&a!==1);if(e===4)return(c=this.K)&&c.state?(d=c.state.status,d!==42&&d!==69&&d!==73&&d!==91&&d!==103&&d!==113&&d!==666?a=ul(c,-2):(c.state=null,a=d===113?ul(c,-3):0)):a=-2,Ml(this,a),this.ended=!0,a===0;e===2&&(Ml(this,0),c.S=0);return!0};
function Ml(a,b){b===0&&(a.result=a.options.to==="string"?a.chunks.join(""):N.td(a.chunks));a.chunks=[];a.err=b;a.msg=a.K.msg}
function Nl(a,b){b=b||{};b.gzip=!0;b=new Ll(b);b.push(a,!0);if(b.err)throw b.msg||Nk[b.err];return b.result}
;function Ol(a){return a?(a=a.privateDoNotAccessOrElseSafeScriptWrappedValue)?Hb(a):null:null}
function Pl(a){return a?(a=a.privateDoNotAccessOrElseTrustedResourceUrlWrappedValue)?ob(a):null:null}
;function Ql(a){return ob(a===null?"null":a===void 0?"undefined":a)}
;function Rl(a){this.name=a}
;var Sl=new Rl("rawColdConfigGroup");var Tl=new Rl("rawHotConfigGroup");function Ul(a){this.F=J(a)}
w(Ul,L);function Vl(a){this.F=J(a)}
w(Vl,L);Vl.prototype.setTrackingParams=function(a){if(a!=null)if(typeof a==="string")a=a?new yd(a,xd):Ad||(Ad=new yd(null,xd));else if(a.constructor!==yd)if(wd(a))a=a.length?new yd(new Uint8Array(a),xd):Ad||(Ad=new yd(null,xd));else throw Error();return gf(this,1,a)};var Wl=new Rl("continuationCommand");var Xl=new Rl("webCommandMetadata");var Yl=new Rl("signalServiceEndpoint");var Zl={Tf:"EMBEDDED_PLAYER_MODE_UNKNOWN",Qf:"EMBEDDED_PLAYER_MODE_DEFAULT",Sf:"EMBEDDED_PLAYER_MODE_PFP",Rf:"EMBEDDED_PLAYER_MODE_PFL"};var $l=new Rl("feedbackEndpoint");var ge={Vg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_UNKNOWN",pg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_FOR_TESTING",Gg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_RESUME_TO_HOME_TTL",Ng:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_START_TO_SHORTS_ANALYSIS_SLICE",fg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_DEVICE_LAYER_SLICE",Ug:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_UNIFIED_LAYER_SLICE",Xg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_VISITOR_LAYER_SLICE",Mg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_SHOW_SHEET_COMMAND_HANDLER_BLOCK",
Zg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_WIZ_NEXT_MIGRATED_COMPONENT",Yg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_WIZ_NEXT_CHANNEL_NAME_TOOLTIP",Jg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ROTATION_LOCK_SUPPORTED",Pg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_THEATER_MODE_ENABLED",eh:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_WOULD_SHOW_PIN_SUGGESTION",dh:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_WOULD_SHOW_LONG_PRESS_EDU_TOAST",bh:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_WOULD_SHOW_AMBIENT",Qg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_TIME_WATCHED_PANEL",
Lg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_SEARCH_FROM_SEARCH_BAR_OVERLAY",fh:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_WOULD_SHOW_VOICE_SEARCH_EDU_TOAST",Og:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_SUGGESTED_LANGUAGE_SELECTED",gh:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_WOULD_TRIGGER_SHORTS_PIP",wg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_IN_ZP_VOICE_CRASHY_SET",Cg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_REEL_FAST_SWIPE_SUPPRESSED",Bg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_REEL_FAST_SWIPE_ALLOWED",Eg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_REEL_PULL_TO_REFRESH_ATTEMPT",
ah:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_WOULD_BLOCK_KABUKI",Fg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_REEL_TALL_SCREEN",Dg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_REEL_NORMAL_SCREEN",Xf:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ACCESSIBILITY_MODE_ENABLED",Wf:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ACCESSIBILITY_MODE_DISABLED",Yf:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_AUTOPLAY_ENABLED",Zf:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_CAST_MATCH_OCCURRED",ig:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_EMC3DS_ELIGIBLE",lg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ENDSCREEN_TRIGGERED",
Ag:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_POSTPLAY_TRIGGERED",zg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_POSTPLAY_LACT_THRESHOLD_EXCEEDED",qg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_IDENTITIES_STATE_MATCHED_ON_REMOTE_CONNECTION",sg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_IDENTITIES_STATE_SWITCHABLE_ON_REMOTE_CONNECTION",rg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_IDENTITIES_STATE_MISATTRIBUTED_ON_REMOTE_CONNECTION",vg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_IDENTITIES_TV_IS_SIGNED_IN_ON_REMOTE_CONNECTION",Sg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_TV_START_TYPE_COLD_ON_REMOTE_CONNECTION",
Tg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_TV_START_TYPE_NON_COLD_ON_REMOTE_CONNECTION",yg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ON_REMOTE_CONNECTION",eg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_COBALT_PERSISTENT_SETTINGS_TEST_VALID",cg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_COBALT_PERSISTENT_SETTINGS_TEST_INVALID",dg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_COBALT_PERSISTENT_SETTINGS_TEST_UNDEFINED",ag:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_COBALT_PERSISTENT_SETTINGS_TEST_DEFINED",xg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_LACT_THRESHOLD_EXCEEDED",
Kg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ROUND_TRIP_HANDLING_ON_REMOTE_CONNECTION",ug:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_IDENTITIES_STATE_SWITCHED_ON_REMOTE_CONNECTION_BEFORE_APP_RELOAD",tg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_IDENTITIES_STATE_SWITCHED_ON_REMOTE_CONNECTION_AFTER_APP_RELOAD",jg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_EMC3DS_INELIGIBLE",Rg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_TVHTML5_MID_ROLL_THRESHOLD_REACHED",ng:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_EXP_COBALT_HTTP3_CONFIG_PENDING",
mg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_EXP_COBALT_HTTP3_CONFIG_ACTIVATED",kg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_EMC3DS_M2_ELIGIBLE",Hg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ROTATE_DEVICE_TO_LANDSCAPE",Ig:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ROTATE_DEVICE_TO_PORTRAIT",hg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_EMBEDS_FACEOFF_UI_EVENT",og:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_EXP_COBALT_HTTP3_CONFIG_RECEIVED",gg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_ELIGIBLE_TO_SUPPRESS_TRANSPORT_CONTROLS_BUTTONS",
Wg:"GENERIC_CLIENT_EXPERIMENT_EVENT_TYPE_USER_HAS_THEATER_MODE_COOKIE_ENABLED"};var am=new Rl("shareEndpoint"),bm=new Rl("shareEntityEndpoint"),cm=new Rl("shareEntityServiceEndpoint"),dm=new Rl("webPlayerShareEntityServiceEndpoint");var em=new Rl("playlistEditEndpoint");var fm=new Rl("modifyChannelNotificationPreferenceEndpoint");var gm=new Rl("unsubscribeEndpoint");var hm=new Rl("subscribeEndpoint");function im(){var a=jm;E("yt.ads.biscotti.getId_")||D("yt.ads.biscotti.getId_",a)}
function km(a){D("yt.ads.biscotti.lastId_",a)}
;function lm(a,b){b.length>1?a[b[0]]=b[1]:b.length===1&&Object.assign(a,b[0])}
;var mm=C.window,nm,om,pm=(mm==null?void 0:(nm=mm.yt)==null?void 0:nm.config_)||(mm==null?void 0:(om=mm.ytcfg)==null?void 0:om.data_)||{};D("yt.config_",pm);function qm(){lm(pm,arguments)}
function P(a,b){return a in pm?pm[a]:b}
function rm(a){var b=pm.EXPERIMENT_FLAGS;return b?b[a]:void 0}
;var sm=[];function tm(a){sm.forEach(function(b){return b(a)})}
function um(a){return a&&window.yterr?function(){try{return a.apply(this,arguments)}catch(b){wm(b)}}:a}
function wm(a){var b=E("yt.logging.errors.log");b?b(a,"ERROR",void 0,void 0,void 0,void 0,void 0):(b=P("ERRORS",[]),b.push([a,"ERROR",void 0,void 0,void 0,void 0,void 0]),qm("ERRORS",b));tm(a)}
function xm(a,b,c,d,e){var f=E("yt.logging.errors.log");f?f(a,"WARNING",b,c,d,void 0,e):(f=P("ERRORS",[]),f.push([a,"WARNING",b,c,d,void 0,e]),qm("ERRORS",f))}
;var ym=/^[\w.]*$/,zm={q:!0,search_query:!0};function Am(a,b){b=a.split(b);for(var c={},d=0,e=b.length;d<e;d++){var f=b[d].split("=");if(f.length===1&&f[0]||f.length===2)try{var g=Bm(f[0]||""),h=Bm(f[1]||"");if(g in c){var k=c[g];Array.isArray(k)?Yb(k,h):c[g]=[k,h]}else c[g]=h}catch(p){var l=p,m=f[0],n=String(Am);l.args=[{key:m,value:f[1],query:a,method:Cm===n?"unchanged":n}];zm.hasOwnProperty(m)||xm(l)}}return c}
var Cm=String(Am);function Dm(a){var b=[];tg(a,function(c,d){var e=encodeURIComponent(String(d));c=Array.isArray(c)?c:[c];Sb(c,function(f){f==""?b.push(e):b.push(e+"="+encodeURIComponent(String(f)))})});
return b.join("&")}
function Em(a){a.charAt(0)==="?"&&(a=a.substring(1));return Am(a,"&")}
function Fm(a){return a.indexOf("?")!==-1?(a=(a||"").split("#")[0],a=a.split("?",2),Em(a.length>1?a[1]:a[0])):{}}
function Gm(a,b){return Hm(a,b||{},!0)}
function Hm(a,b,c){var d=a.split("#",2);a=d[0];d=d.length>1?"#"+d[1]:"";var e=a.split("?",2);a=e[0];e=Em(e[1]||"");for(var f in b)!c&&e!==null&&f in e||(e[f]=b[f]);return nc(a,e)+d}
function Im(a){if(!b)var b=window.location.href;var c=hc(1,a),d=ic(a);c&&d?(a=a.match(ec),b=b.match(ec),a=a[3]==b[3]&&a[1]==b[1]&&a[4]==b[4]):a=d?ic(b)===d&&(Number(hc(4,b))||null)===(Number(hc(4,a))||null):!0;return a}
function Bm(a){return a&&a.match(ym)?a:decodeURIComponent(a.replace(/\+/g," "))}
;function Jm(a){var b=Km;a=a===void 0?E("yt.ads.biscotti.lastId_")||"":a;var c=Object,d=c.assign,e={};e.dt=Vj;e.flash="0";a:{try{var f=b.h.top.location.href}catch(Pa){f=2;break a}f=f?f===b.i.location.href?0:1:2}e=(e.frm=f,e);try{e.u_tz=-(new Date).getTimezoneOffset();var g=g===void 0?Nj:g;try{var h=g.history.length}catch(Pa){h=0}e.u_his=h;var k;e.u_h=(k=Nj.screen)==null?void 0:k.height;var l;e.u_w=(l=Nj.screen)==null?void 0:l.width;var m;e.u_ah=(m=Nj.screen)==null?void 0:m.availHeight;var n;e.u_aw=
(n=Nj.screen)==null?void 0:n.availWidth;var p;e.u_cd=(p=Nj.screen)==null?void 0:p.colorDepth}catch(Pa){}h=b.h;try{var t=h.screenX;var u=h.screenY}catch(Pa){}try{var x=h.outerWidth;var z=h.outerHeight}catch(Pa){}try{var H=h.innerWidth;var K=h.innerHeight}catch(Pa){}try{var da=h.screenLeft;var ea=h.screenTop}catch(Pa){}try{H=h.innerWidth,K=h.innerHeight}catch(Pa){}try{var Oa=h.screen.availWidth;var Ob=h.screen.availTop}catch(Pa){}t=[da,ea,t,u,Oa,Ob,x,z,H,K];try{var ka=(b.h.top||window).document,Ya=
ka.compatMode=="CSS1Compat"?ka.documentElement:ka.body;var Qa=(new sg(Ya.clientWidth,Ya.clientHeight)).round()}catch(Pa){Qa=new sg(-12245933,-12245933)}ka=Qa;Qa={};var Ra=Ra===void 0?C:Ra;Ya=new bk;"SVGElement"in Ra&&"createElementNS"in Ra.document&&Ya.set(0);u=Sj();u["allow-top-navigation-by-user-activation"]&&Ya.set(1);u["allow-popups-to-escape-sandbox"]&&Ya.set(2);Ra.crypto&&Ra.crypto.subtle&&Ya.set(3);"TextDecoder"in Ra&&"TextEncoder"in Ra&&Ya.set(4);Ra=ck(Ya);Qa.bc=Ra;Qa.bih=ka.height;Qa.biw=
ka.width;Qa.brdim=t.join();b=b.i;b=(Qa.vis=b.prerendering?3:{visible:1,hidden:2,prerender:3,preview:4,unloaded:5}[b.visibilityState||b.webkitVisibilityState||b.mozVisibilityState||""]||0,Qa.wgl=!!Nj.WebGLRenderingContext,Qa);c=d.call(c,e,b);c.ca_type="image";a&&(c.bid=a);return c}
var Km=new function(){var a=window.document;this.h=window;this.i=a};
D("yt.ads_.signals_.getAdSignalsString",function(a){return Dm(Jm(a))});ab();navigator.userAgent.indexOf(" (CrKey ");var Lm="XMLHttpRequest"in C?function(){return new XMLHttpRequest}:null;
function Mm(){if(!Lm)return null;var a=Lm();return"open"in a?a:null}
function Nm(a){switch(a&&"status"in a?a.status:-1){case 200:case 201:case 202:case 203:case 204:case 205:case 206:case 304:return!0;default:return!1}}
;function Om(a,b){typeof a==="function"&&(a=um(a));return window.setTimeout(a,b)}
;var Pm="client_dev_domain client_dev_expflag client_dev_regex_map client_dev_root_url client_rollout_override expflag forcedCapability jsfeat jsmode mods".split(" ");[].concat(ra(Pm),["client_dev_set_cookie"]);function R(a){a=Qm(a);return typeof a==="string"&&a==="false"?!1:!!a}
function S(a,b){a=Qm(a);return a===void 0&&b!==void 0?b:Number(a||0)}
function Qm(a){return P("EXPERIMENT_FLAGS",{})[a]}
function Rm(){for(var a=[],b=P("EXPERIMENTS_FORCED_FLAGS",{}),c=y(Object.keys(b)),d=c.next();!d.done;d=c.next())d=d.value,a.push({key:d,value:String(b[d])});c=P("EXPERIMENT_FLAGS",{});d=y(Object.keys(c));for(var e=d.next();!e.done;e=d.next())e=e.value,e.startsWith("force_")&&b[e]===void 0&&a.push({key:e,value:String(c[e])});return a}
;var Sm={Authorization:"AUTHORIZATION","X-Goog-EOM-Visitor-Id":"EOM_VISITOR_DATA","X-Goog-Visitor-Id":"SANDBOXED_VISITOR_ID","X-Youtube-Domain-Admin-State":"DOMAIN_ADMIN_STATE","X-Youtube-Chrome-Connected":"CHROME_CONNECTED_HEADER","X-YouTube-Client-Name":"INNERTUBE_CONTEXT_CLIENT_NAME","X-YouTube-Client-Version":"INNERTUBE_CONTEXT_CLIENT_VERSION","X-YouTube-Delegation-Context":"INNERTUBE_CONTEXT_SERIALIZED_DELEGATION_CONTEXT","X-YouTube-Device":"DEVICE","X-Youtube-Identity-Token":"ID_TOKEN","X-YouTube-Page-CL":"PAGE_CL",
"X-YouTube-Page-Label":"PAGE_BUILD_LABEL","X-Goog-AuthUser":"SESSION_INDEX","X-Goog-PageId":"DELEGATED_SESSION_ID"},Tm="app debugcss debugjs expflag force_ad_params force_ad_encrypted force_viral_ad_response_params forced_experiments innertube_snapshots innertube_goldens internalcountrycode internalipoverride absolute_experiments conditional_experiments sbb sr_bns_address".split(" ").concat(ra(Pm)),Um=!1;function Vm(a,b,c,d,e,f,g,h){function k(){(l&&"readyState"in l?l.readyState:0)===4&&b&&um(b)(l)}
c=c===void 0?"GET":c;d=d===void 0?"":d;h=h===void 0?!1:h;var l=Mm();if(!l)return null;"onloadend"in l?l.addEventListener("loadend",k,!1):l.onreadystatechange=k;R("debug_forward_web_query_parameters")&&(a=Wm(a));l.open(c,a,!0);f&&(l.responseType=f);g&&(l.withCredentials=!0);c=c==="POST"&&(window.FormData===void 0||!(d instanceof FormData));if(e=Xm(a,e))for(var m in e)l.setRequestHeader(m,e[m]),"content-type"===m.toLowerCase()&&(c=!1);c&&l.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
if(h&&"setAttributionReporting"in XMLHttpRequest.prototype){a={eventSourceEligible:!0,triggerEligible:!1};try{l.setAttributionReporting(a)}catch(n){xm(n)}}l.send(d);return l}
function Xm(a,b){b=b===void 0?{}:b;var c=Im(a),d=P("INNERTUBE_CLIENT_NAME"),e=R("web_ajax_ignore_global_headers_if_set"),f;for(f in Sm){var g=P(Sm[f]),h=f==="X-Goog-AuthUser"||f==="X-Goog-PageId";f!=="X-Goog-Visitor-Id"||g||(g=P("VISITOR_DATA"));var k;if(!(k=!g)){if(!(k=c||(ic(a)?!1:!0))){k=a;var l;if(l=R("add_auth_headers_to_remarketing_google_dot_com_ping")&&f==="Authorization"&&(d==="TVHTML5"||d==="TVHTML5_UNPLUGGED"||d==="TVHTML5_SIMPLY"))l=ic(k),l=l!==null?l.split(".").reverse():null,l=l===null?
!1:l[1]==="google"?!0:l[2]==="google"?l[0]==="au"&&l[1]==="com"?!0:l[0]==="uk"&&l[1]==="co"?!0:!1:!1;l&&(k=fc(hc(5,k))||"",k=k.split("/"),k="/"+(k.length>1?k[1]:""),l=k==="/pagead");k=l?!0:!1}k=!k}k||e&&b[f]!==void 0||d==="TVHTML5_UNPLUGGED"&&h||(b[f]=g)}"X-Goog-EOM-Visitor-Id"in b&&"X-Goog-Visitor-Id"in b&&delete b["X-Goog-Visitor-Id"];if(c||!ic(a))b["X-YouTube-Utc-Offset"]=String(-(new Date).getTimezoneOffset());if(c||!ic(a)){try{var m=(new Intl.DateTimeFormat).resolvedOptions().timeZone}catch(n){}m&&
(b["X-YouTube-Time-Zone"]=m)}document.location.hostname.endsWith("youtubeeducation.com")||!c&&ic(a)||(b["X-YouTube-Ad-Signals"]=Dm(Jm()));return b}
function Ym(a,b){b.method="POST";b.postParams||(b.postParams={});return Zm(a,b)}
function Zm(a,b){var c=b.format||"JSON";a=$m(a,b);var d=an(a,b),e=!1,f=bn(a,function(k){if(!e){e=!0;h&&window.clearTimeout(h);var l=Nm(k),m=null,n=400<=k.status&&k.status<500,p=500<=k.status&&k.status<600;if(l||n||p)m=cn(a,c,k,b.convertToSafeHtml);l&&(l=dn(c,k,m));m=m||{};n=b.context||C;l?b.onSuccess&&b.onSuccess.call(n,k,m):b.onError&&b.onError.call(n,k,m);b.onFinish&&b.onFinish.call(n,k,m)}},b.method,d,b.headers,b.responseType,b.withCredentials);
d=b.timeout||0;if(b.onTimeout&&d>0){var g=b.onTimeout;var h=Om(function(){e||(e=!0,f.abort(),window.clearTimeout(h),g.call(b.context||C,f))},d)}return f}
function $m(a,b){b.includeDomain&&(a=document.location.protocol+"//"+document.location.hostname+(document.location.port?":"+document.location.port:"")+a);var c=P("XSRF_FIELD_NAME");if(b=b.urlParams)b[c]&&delete b[c],a=Gm(a,b);return a}
function an(a,b){var c=P("XSRF_FIELD_NAME"),d=P("XSRF_TOKEN"),e=b.postBody||"",f=b.postParams,g=P("XSRF_FIELD_NAME"),h;b.headers&&(h=b.headers["Content-Type"]);b.excludeXsrf||ic(a)&&!b.withCredentials&&ic(a)!==document.location.hostname||b.method!=="POST"||h&&h!=="application/x-www-form-urlencoded"||b.postParams&&b.postParams[g]||(f||(f={}),f[c]=d);(R("ajax_parse_query_data_only_when_filled")&&f&&Object.keys(f).length>0||f)&&typeof e==="string"&&(e=Em(e),Dg(e,f),e=b.postBodyFormat&&b.postBodyFormat===
"JSON"?JSON.stringify(e):mc(e));f=e||f&&!wg(f);!Um&&f&&b.method!=="POST"&&(Um=!0,wm(Error("AJAX request with postData should use POST")));return e}
function cn(a,b,c,d){var e=null;switch(b){case "JSON":try{var f=c.responseText}catch(g){throw d=Error("Error reading responseText"),d.params=a,xm(d),g;}a=c.getResponseHeader("Content-Type")||"";f&&a.indexOf("json")>=0&&(f.substring(0,5)===")]}'\n"&&(f=f.substring(5)),e=JSON.parse(f));break;case "XML":if(a=(a=c.responseXML)?en(a):null)e={},Sb(a.getElementsByTagName("*"),function(g){e[g.tagName]=fn(g)})}d&&gn(e);
return e}
function gn(a){if(Sa(a))for(var b in a){var c;(c=b==="html_content")||(c=b.length-5,c=c>=0&&b.indexOf("_html",c)==c);if(c){c=b;var d=a[b];var e=mb();d=new Eb(e?e.createHTML(d):d);a[c]=d}else gn(a[b])}}
function dn(a,b,c){if(b&&b.status===204)return!0;switch(a){case "JSON":return!!c;case "XML":return Number(c&&c.return_code)===0;case "RAW":return!0;default:return!!c}}
function en(a){return a?(a=("responseXML"in a?a.responseXML:a).getElementsByTagName("root"))&&a.length>0?a[0]:null:null}
function fn(a){var b="";Sb(a.childNodes,function(c){b+=c.nodeValue});
return b}
function Wm(a){var b=window.location.search,c=ic(a);R("debug_handle_relative_url_for_query_forward_killswitch")||!c&&Im(a)&&(c=document.location.hostname);var d=fc(hc(5,a));d=(c=c&&(c.endsWith("youtube.com")||c.endsWith("youtube-nocookie.com")))&&d&&d.startsWith("/api/");if(!c||d)return a;var e=Em(b),f={};Sb(Tm,function(g){e[g]&&(f[g]=e[g])});
return Hm(a,f||{},!1)}
var bn=Vm;var hn=[{Rc:function(a){return"Cannot read property '"+a.key+"'"},
vc:{Error:[{regexp:/(Permission denied) to access property "([^']+)"/,groups:["reason","key"]}],TypeError:[{regexp:/Cannot read property '([^']+)' of (null|undefined)/,groups:["key","value"]},{regexp:/\u65e0\u6cd5\u83b7\u53d6\u672a\u5b9a\u4e49\u6216 (null|undefined) \u5f15\u7528\u7684\u5c5e\u6027\u201c([^\u201d]+)\u201d/,groups:["value","key"]},{regexp:/\uc815\uc758\ub418\uc9c0 \uc54a\uc74c \ub610\ub294 (null|undefined) \ucc38\uc870\uc778 '([^']+)' \uc18d\uc131\uc744 \uac00\uc838\uc62c \uc218 \uc5c6\uc2b5\ub2c8\ub2e4./,
groups:["value","key"]},{regexp:/No se puede obtener la propiedad '([^']+)' de referencia nula o sin definir/,groups:["key"]},{regexp:/Unable to get property '([^']+)' of (undefined or null) reference/,groups:["key","value"]},{regexp:/(null) is not an object \(evaluating '(?:([^.]+)\.)?([^']+)'\)/,groups:["value","base","key"]}]}},{Rc:function(a){return"Cannot call '"+a.key+"'"},
vc:{TypeError:[{regexp:/(?:([^ ]+)?\.)?([^ ]+) is not a function/,groups:["base","key"]},{regexp:/([^ ]+) called on (null or undefined)/,groups:["key","value"]},{regexp:/Object (.*) has no method '([^ ]+)'/,groups:["base","key"]},{regexp:/Object doesn't support property or method '([^ ]+)'/,groups:["key"]},{regexp:/\u30aa\u30d6\u30b8\u30a7\u30af\u30c8\u306f '([^']+)' \u30d7\u30ed\u30d1\u30c6\u30a3\u307e\u305f\u306f\u30e1\u30bd\u30c3\u30c9\u3092\u30b5\u30dd\u30fc\u30c8\u3057\u3066\u3044\u307e\u305b\u3093/,
groups:["key"]},{regexp:/\uac1c\uccb4\uac00 '([^']+)' \uc18d\uc131\uc774\ub098 \uba54\uc11c\ub4dc\ub97c \uc9c0\uc6d0\ud558\uc9c0 \uc54a\uc2b5\ub2c8\ub2e4./,groups:["key"]}]}},{Rc:function(a){return a.key+" is not defined"},
vc:{ReferenceError:[{regexp:/(.*) is not defined/,groups:["key"]},{regexp:/Can't find variable: (.*)/,groups:["key"]}]}}];var kn={Ya:[],Ta:[{callback:jn,weight:500}]};function jn(a){if(a.name==="JavaException")return!0;a=a.stack;return a.includes("chrome://")||a.includes("chrome-extension://")||a.includes("moz-extension://")}
;function ln(){this.Ta=[];this.Ya=[]}
var mn;function nn(){if(!mn){var a=mn=new ln;a.Ya.length=0;a.Ta.length=0;kn.Ya&&a.Ya.push.apply(a.Ya,kn.Ya);kn.Ta&&a.Ta.push.apply(a.Ta,kn.Ta)}return mn}
;var on=new M;function pn(a){function b(){return a.charCodeAt(d++)}
var c=a.length,d=0;do{var e=qn(b);if(e===Infinity)break;var f=e>>3;switch(e&7){case 0:e=qn(b);if(f===2)return e;break;case 1:if(f===2)return;d+=8;break;case 2:e=qn(b);if(f===2)return a.substr(d,e);d+=e;break;case 5:if(f===2)return;d+=4;break;default:return}}while(d<c)}
function qn(a){var b=a(),c=b&127;if(b<128)return c;b=a();c|=(b&127)<<7;if(b<128)return c;b=a();c|=(b&127)<<14;if(b<128)return c;b=a();return b<128?c|(b&127)<<21:Infinity}
;function rn(a,b,c,d){if(a)if(Array.isArray(a)){var e=d;for(d=0;d<a.length&&!(a[d]&&(e+=sn(d,a[d],b,c),e>500));d++);d=e}else if(typeof a==="object")for(e in a){if(a[e]){var f=e;var g=a[e],h=b,k=c;f=typeof g!=="string"||f!=="clickTrackingParams"&&f!=="trackingParams"?0:(g=pn(atob(g.replace(/-/g,"+").replace(/_/g,"/"))))?sn(f+".ve",g,h,k):0;d+=f;d+=sn(e,a[e],b,c);if(d>500)break}}else c[b]=tn(a),d+=c[b].length;else c[b]=tn(a),d+=c[b].length;return d}
function sn(a,b,c,d){c+="."+a;a=tn(b);d[c]=a;return c.length+a.length}
function tn(a){try{return(typeof a==="string"?a:String(JSON.stringify(a))).substr(0,500)}catch(b){return"unable to serialize "+typeof a+" ("+b.message+")"}}
;function un(a){var b=this;this.i=void 0;this.h=!1;a.addEventListener("beforeinstallprompt",function(c){c.preventDefault();b.i=c});
a.addEventListener("appinstalled",function(){b.h=!0},{once:!0})}
function vn(){if(!C.matchMedia)return"WEB_DISPLAY_MODE_UNKNOWN";try{return C.matchMedia("(display-mode: standalone)").matches?"WEB_DISPLAY_MODE_STANDALONE":C.matchMedia("(display-mode: minimal-ui)").matches?"WEB_DISPLAY_MODE_MINIMAL_UI":C.matchMedia("(display-mode: fullscreen)").matches?"WEB_DISPLAY_MODE_FULLSCREEN":C.matchMedia("(display-mode: browser)").matches?"WEB_DISPLAY_MODE_BROWSER":"WEB_DISPLAY_MODE_UNKNOWN"}catch(a){return"WEB_DISPLAY_MODE_UNKNOWN"}}
;function wn(){this.Md=!0}
function xn(){wn.h||(wn.h=new wn);return wn.h}
function yn(a,b){a={};var c=[];"USER_SESSION_ID"in pm&&c.push({key:"u",value:P("USER_SESSION_ID")});if(c=lg(c))a.Authorization=c,c=b=b==null?void 0:b.sessionIndex,c===void 0&&(c=Number(P("SESSION_INDEX",0)),c=isNaN(c)?0:c),R("voice_search_auth_header_removal")||(a["X-Goog-AuthUser"]=c.toString()),"INNERTUBE_HOST_OVERRIDE"in pm||(a["X-Origin"]=window.location.origin),b===void 0&&"DELEGATED_SESSION_ID"in pm&&(a["X-Goog-PageId"]=P("DELEGATED_SESSION_ID"));return a}
;var zn={identityType:"UNAUTHENTICATED_IDENTITY_TYPE_UNKNOWN"};function An(a,b,c,d,e){ig.set(""+a,b,{Tb:c,path:"/",domain:d===void 0?"youtube.com":d,secure:e===void 0?!1:e})}
function Bn(a){return ig.get(""+a,void 0)}
function Cn(a,b,c){ig.remove(""+a,b===void 0?"/":b,c===void 0?"youtube.com":c)}
function Dn(){if(R("embeds_web_enable_cookie_detection_fix")){if(!C.navigator.cookieEnabled)return!1}else if(!ig.isEnabled())return!1;if(ig.h.cookie)return!0;R("embeds_web_enable_cookie_detection_fix")?ig.set("TESTCOOKIESENABLED","1",{Tb:60,cf:"none",secure:!0}):ig.set("TESTCOOKIESENABLED","1",{Tb:60});if(ig.get("TESTCOOKIESENABLED")!=="1")return!1;ig.remove("TESTCOOKIESENABLED");return!0}
;var En=E("ytglobal.prefsUserPrefsPrefs_")||{};D("ytglobal.prefsUserPrefsPrefs_",En);function Fn(){this.h=P("ALT_PREF_COOKIE_NAME","PREF");this.i=P("ALT_PREF_COOKIE_DOMAIN","youtube.com");var a=Bn(this.h);a&&this.parse(a)}
var Gn;function Hn(){Gn||(Gn=new Fn);return Gn}
r=Fn.prototype;r.get=function(a,b){In(a);Jn(a);a=En[a]!==void 0?En[a].toString():null;return a!=null?a:b?b:""};
r.set=function(a,b){In(a);Jn(a);if(b==null)throw Error("ExpectedNotNull");En[a]=b.toString()};
function Kn(a){return!!((Ln("f"+(Math.floor(a/31)+1))||0)&1<<a%31)}
r.remove=function(a){In(a);Jn(a);delete En[a]};
r.clear=function(){for(var a in En)delete En[a]};
function Jn(a){if(/^f([1-9][0-9]*)$/.test(a))throw Error("ExpectedRegexMatch: "+a);}
function In(a){if(!/^\w+$/.test(a))throw Error("ExpectedRegexMismatch: "+a);}
function Ln(a){a=En[a]!==void 0?En[a].toString():null;return a!=null&&/^[A-Fa-f0-9]+$/.test(a)?parseInt(a,16):null}
r.parse=function(a){a=decodeURIComponent(a).split("&");for(var b=0;b<a.length;b++){var c=a[b].split("="),d=c[0];(c=c[1])&&(En[d]=c.toString())}};var Mn={bluetooth:"CONN_DISCO",cellular:"CONN_CELLULAR_UNKNOWN",ethernet:"CONN_WIFI",none:"CONN_NONE",wifi:"CONN_WIFI",wimax:"CONN_CELLULAR_4G",other:"CONN_UNKNOWN",unknown:"CONN_UNKNOWN","slow-2g":"CONN_CELLULAR_2G","2g":"CONN_CELLULAR_2G","3g":"CONN_CELLULAR_3G","4g":"CONN_CELLULAR_4G"},Nn={"slow-2g":"EFFECTIVE_CONNECTION_TYPE_SLOW_2G","2g":"EFFECTIVE_CONNECTION_TYPE_2G","3g":"EFFECTIVE_CONNECTION_TYPE_3G","4g":"EFFECTIVE_CONNECTION_TYPE_4G"};
function On(){var a=C.navigator;return a?a.connection:void 0}
function Pn(){var a=On();if(a){var b=Mn[a.type||"unknown"]||"CONN_UNKNOWN";a=Mn[a.effectiveType||"unknown"]||"CONN_UNKNOWN";b==="CONN_CELLULAR_UNKNOWN"&&a!=="CONN_UNKNOWN"&&(b=a);if(b!=="CONN_UNKNOWN")return b;if(a!=="CONN_UNKNOWN")return a}}
function Qn(){var a=On();if(a!=null&&a.effectiveType)return Nn.hasOwnProperty(a.effectiveType)?Nn[a.effectiveType]:"EFFECTIVE_CONNECTION_TYPE_UNKNOWN"}
;function T(a){var b=B.apply(1,arguments);var c=Error.call(this,a);this.message=c.message;"stack"in c&&(this.stack=c.stack);this.args=[].concat(ra(b))}
w(T,Error);function Rn(){try{return Sn(),!0}catch(a){return!1}}
function Sn(a){if(P("DATASYNC_ID")!==void 0)return P("DATASYNC_ID");throw new T("Datasync ID not set",a===void 0?"unknown":a);}
;function Tn(){}
function Un(a,b){return ak.Ra(a,0,b)}
Tn.prototype.ma=function(a,b){return this.Ra(a,1,b)};
Tn.prototype.Jb=function(a){var b=E("yt.scheduler.instance.addImmediateJob");b?b(a):a()};var Vn=S("web_emulated_idle_callback_delay",300),Wn=1E3/60-3,Xn=[8,5,4,3,2,1,0];
function Yn(a){a=a===void 0?{}:a;F.call(this);this.i=[];this.j={};this.Z=this.h=0;this.Y=this.u=!1;this.P=[];this.U=this.ha=!1;for(var b=y(Xn),c=b.next();!c.done;c=b.next())this.i[c.value]=[];this.o=0;this.Fc=a.timeout||1;this.G=Wn;this.D=0;this.xa=this.Pe.bind(this);this.Ec=this.wf.bind(this);this.Pa=this.Zd.bind(this);this.Qa=this.Ae.bind(this);this.rb=this.We.bind(this);this.Fa=!!window.requestIdleCallback&&!!window.cancelIdleCallback&&!R("disable_scheduler_requestIdleCallback");(this.pa=a.useRaf!==
!1&&!!window.requestAnimationFrame)&&document.addEventListener("visibilitychange",this.xa)}
w(Yn,F);r=Yn.prototype;r.Jb=function(a){var b=ab();Zn(this,a);a=ab()-b;this.u||(this.G-=a)};
r.Ra=function(a,b,c){++this.Z;if(b===10)return this.Jb(a),this.Z;var d=this.Z;this.j[d]=a;this.u&&!c?this.P.push({id:d,priority:b}):(this.i[b].push(d),this.Y||this.u||(this.h!==0&&$n(this)!==this.D&&this.stop(),this.start()));return d};
r.qa=function(a){delete this.j[a]};
function ao(a){a.P.length=0;for(var b=5;b>=0;b--)a.i[b].length=0;a.i[8].length=0;a.j={};a.stop()}
r.isHidden=function(){return!!document.hidden||!1};
function bo(a){return!a.isHidden()&&a.pa}
function $n(a){if(a.i[8].length){if(a.U)return 4;if(bo(a))return 3}for(var b=5;b>=a.o;b--)if(a.i[b].length>0)return b>0?bo(a)?3:2:1;return 0}
r.Ha=function(a){var b=E("yt.logging.errors.log");b&&b(a)};
function Zn(a,b){try{b()}catch(c){a.Ha(c)}}
function co(a){for(var b=y(Xn),c=b.next();!c.done;c=b.next())if(a.i[c.value].length)return!0;return!1}
r.Ae=function(a){var b=void 0;a&&(b=a.timeRemaining());this.ha=!0;eo(this,b);this.ha=!1};
r.wf=function(){eo(this)};
r.Zd=function(){fo(this)};
r.We=function(a){this.U=!0;var b=$n(this);b===4&&b!==this.D&&(this.stop(),this.start());eo(this,void 0,a);this.U=!1};
r.Pe=function(){this.isHidden()||fo(this);this.h&&(this.stop(),this.start())};
function fo(a){a.stop();a.u=!0;for(var b=ab(),c=a.i[8];c.length;){var d=c.shift(),e=a.j[d];delete a.j[d];e&&Zn(a,e)}go(a);a.u=!1;co(a)&&a.start();b=ab()-b;a.G-=b}
function go(a){for(var b=0,c=a.P.length;b<c;b++){var d=a.P[b];a.i[d.priority].push(d.id)}a.P.length=0}
function eo(a,b,c){a.U&&a.D===4&&a.h||a.stop();a.u=!0;b=ab()+(b||a.G);for(var d=a.i[5];d.length;){var e=d.shift(),f=a.j[e];delete a.j[e];if(f){e=a;try{f(c)}catch(l){e.Ha(l)}}}for(d=a.i[4];d.length;)c=d.shift(),f=a.j[c],delete a.j[c],f&&Zn(a,f);d=a.ha?0:1;d=a.o>d?a.o:d;if(!(ab()>=b)){do{a:{c=a;f=d;for(e=3;e>=f;e--)for(var g=c.i[e];g.length;){var h=g.shift(),k=c.j[h];delete c.j[h];if(k){c=k;break a}}c=null}c&&Zn(a,c)}while(c&&ab()<b)}a.u=!1;go(a);a.G=Wn;co(a)&&a.start()}
r.start=function(){this.Y=!1;if(this.h===0)switch(this.D=$n(this),this.D){case 1:var a=this.Qa;this.h=this.Fa?window.requestIdleCallback(a,{timeout:3E3}):window.setTimeout(a,Vn);break;case 2:this.h=window.setTimeout(this.Ec,this.Fc);break;case 3:this.h=window.requestAnimationFrame(this.rb);break;case 4:this.h=window.setTimeout(this.Pa,0)}};
r.pause=function(){this.stop();this.Y=!0};
r.stop=function(){if(this.h){switch(this.D){case 1:var a=this.h;this.Fa?window.cancelIdleCallback(a):window.clearTimeout(a);break;case 2:case 4:window.clearTimeout(this.h);break;case 3:window.cancelAnimationFrame(this.h)}this.h=0}};
r.ba=function(){ao(this);this.stop();this.pa&&document.removeEventListener("visibilitychange",this.xa);F.prototype.ba.call(this)};var ho=E("yt.scheduler.instance.timerIdMap_")||{},io=S("kevlar_tuner_scheduler_soft_state_timer_ms",800),jo=0,ko=0;function lo(){var a=E("ytglobal.schedulerInstanceInstance_");if(!a||a.ea)a=new Yn(P("scheduler")||{}),D("ytglobal.schedulerInstanceInstance_",a);return a}
function mo(){no();var a=E("ytglobal.schedulerInstanceInstance_");a&&(zc(a),D("ytglobal.schedulerInstanceInstance_",null))}
function no(){ao(lo());for(var a in ho)ho.hasOwnProperty(a)&&delete ho[Number(a)]}
function oo(a,b,c){if(!c)return c=c===void 0,-lo().Ra(a,b,c);var d=window.setTimeout(function(){var e=lo().Ra(a,b);ho[d]=e},c);
return d}
function po(a){lo().Jb(a)}
function qo(a){var b=lo();if(a<0)b.qa(-a);else{var c=ho[a];c?(b.qa(c),delete ho[a]):window.clearTimeout(a)}}
function ro(){so()}
function so(){window.clearTimeout(jo);lo().start()}
function to(){lo().pause();window.clearTimeout(jo);jo=window.setTimeout(ro,io)}
function uo(){window.clearTimeout(ko);ko=window.setTimeout(function(){vo(0)},io)}
function vo(a){uo();var b=lo();b.o=a;b.start()}
function wo(a){uo();var b=lo();b.o>a&&(b.o=a,b.start())}
function xo(){window.clearTimeout(ko);var a=lo();a.o=0;a.start()}
;function yo(){Tn.apply(this,arguments)}
w(yo,Tn);function zo(){yo.h||(yo.h=new yo);return yo.h}
yo.prototype.Ra=function(a,b,c){c!==void 0&&Number.isNaN(Number(c))&&(c=void 0);var d=E("yt.scheduler.instance.addJob");return d?d(a,b,c):c===void 0?(a(),NaN):Om(a,c||0)};
yo.prototype.qa=function(a){if(a===void 0||!Number.isNaN(Number(a))){var b=E("yt.scheduler.instance.cancelJob");b?b(a):window.clearTimeout(a)}};
yo.prototype.start=function(){var a=E("yt.scheduler.instance.start");a&&a()};
yo.prototype.pause=function(){var a=E("yt.scheduler.instance.pause");a&&a()};
var ak=zo();
R("web_scheduler_auto_init")&&!E("yt.scheduler.initialized")&&(D("yt.scheduler.instance.dispose",mo),D("yt.scheduler.instance.addJob",oo),D("yt.scheduler.instance.addImmediateJob",po),D("yt.scheduler.instance.cancelJob",qo),D("yt.scheduler.instance.cancelAllJobs",no),D("yt.scheduler.instance.start",so),D("yt.scheduler.instance.pause",to),D("yt.scheduler.instance.setPriorityThreshold",vo),D("yt.scheduler.instance.enablePriorityThreshold",wo),D("yt.scheduler.instance.clearPriorityThreshold",xo),D("yt.scheduler.initialized",
!0));function Ao(a){var b=new Ak;this.h=(a=b.isAvailable()?a?new Bk(b,a):b:null)?new vk(a):null;this.i=document.domain||window.location.hostname}
Ao.prototype.set=function(a,b,c,d){c=c||31104E3;this.remove(a);if(this.h)try{this.h.set(a,b,Date.now()+c*1E3);return}catch(f){}var e="";if(d)try{e=escape((new Ri).serialize(b))}catch(f){return}else e=escape(b);An(a,e,c,this.i)};
Ao.prototype.get=function(a,b){var c=void 0,d=!this.h;if(!d)try{c=this.h.get(a)}catch(e){d=!0}if(d&&(c=Bn(a))&&(c=unescape(c),b))try{c=JSON.parse(c)}catch(e){this.remove(a),c=void 0}return c};
Ao.prototype.remove=function(a){this.h&&this.h.remove(a);Cn(a,"/",this.i)};var Bo=function(){var a;return function(){a||(a=new Ao("ytidb"));return a}}();
function Co(){var a;return(a=Bo())==null?void 0:a.get("LAST_RESULT_ENTRY_KEY",!0)}
;var Do=[],Eo,Fo=!1;function Go(){var a={};for(Eo=new Ho(a.handleError===void 0?Io:a.handleError,a.logEvent===void 0?Jo:a.logEvent);Do.length>0;)switch(a=Do.shift(),a.type){case "ERROR":Eo.Ha(a.payload);break;case "EVENT":Eo.logEvent(a.eventType,a.payload)}}
function Ko(a){Fo||(Eo?Eo.Ha(a):(Do.push({type:"ERROR",payload:a}),Do.length>10&&Do.shift()))}
function Lo(a,b){Fo||(Eo?Eo.logEvent(a,b):(Do.push({type:"EVENT",eventType:a,payload:b}),Do.length>10&&Do.shift()))}
;function Mo(a){if(a.indexOf(":")>=0)throw Error("Database name cannot contain ':'");}
function No(a){return a.substr(0,a.indexOf(":"))||a}
;var Oo=hd||id;function Po(a){var b=Sc();return b?b.toLowerCase().indexOf(a)>=0:!1}
;var Qo={},Ro=(Qo.AUTH_INVALID="No user identifier specified.",Qo.EXPLICIT_ABORT="Transaction was explicitly aborted.",Qo.IDB_NOT_SUPPORTED="IndexedDB is not supported.",Qo.MISSING_INDEX="Index not created.",Qo.MISSING_OBJECT_STORES="Object stores not created.",Qo.DB_DELETED_BY_MISSING_OBJECT_STORES="Database is deleted because expected object stores were not created.",Qo.DB_REOPENED_BY_MISSING_OBJECT_STORES="Database is reopened because expected object stores were not created.",Qo.UNKNOWN_ABORT="Transaction was aborted for unknown reasons.",
Qo.QUOTA_EXCEEDED="The current transaction exceeded its quota limitations.",Qo.QUOTA_MAYBE_EXCEEDED="The current transaction may have failed because of exceeding quota limitations.",Qo.EXECUTE_TRANSACTION_ON_CLOSED_DB="Can't start a transaction on a closed database",Qo.INCOMPATIBLE_DB_VERSION="The binary is incompatible with the database version",Qo),So={},To=(So.AUTH_INVALID="ERROR",So.EXECUTE_TRANSACTION_ON_CLOSED_DB="WARNING",So.EXPLICIT_ABORT="IGNORED",So.IDB_NOT_SUPPORTED="ERROR",So.MISSING_INDEX=
"WARNING",So.MISSING_OBJECT_STORES="ERROR",So.DB_DELETED_BY_MISSING_OBJECT_STORES="WARNING",So.DB_REOPENED_BY_MISSING_OBJECT_STORES="WARNING",So.QUOTA_EXCEEDED="WARNING",So.QUOTA_MAYBE_EXCEEDED="WARNING",So.UNKNOWN_ABORT="WARNING",So.INCOMPATIBLE_DB_VERSION="WARNING",So),Uo={},Vo=(Uo.AUTH_INVALID=!1,Uo.EXECUTE_TRANSACTION_ON_CLOSED_DB=!1,Uo.EXPLICIT_ABORT=!1,Uo.IDB_NOT_SUPPORTED=!1,Uo.MISSING_INDEX=!1,Uo.MISSING_OBJECT_STORES=!1,Uo.DB_DELETED_BY_MISSING_OBJECT_STORES=!1,Uo.DB_REOPENED_BY_MISSING_OBJECT_STORES=
!1,Uo.QUOTA_EXCEEDED=!1,Uo.QUOTA_MAYBE_EXCEEDED=!0,Uo.UNKNOWN_ABORT=!0,Uo.INCOMPATIBLE_DB_VERSION=!1,Uo);function Wo(a,b,c,d,e){b=b===void 0?{}:b;c=c===void 0?Ro[a]:c;d=d===void 0?To[a]:d;e=e===void 0?Vo[a]:e;T.call(this,c,Object.assign({},{name:"YtIdbKnownError",isSw:self.document===void 0,isIframe:self!==self.top,type:a},b));this.type=a;this.message=c;this.level=d;this.h=e;Object.setPrototypeOf(this,Wo.prototype)}
w(Wo,T);function Xo(a,b){Wo.call(this,"MISSING_OBJECT_STORES",{expectedObjectStores:b,foundObjectStores:a},Ro.MISSING_OBJECT_STORES);Object.setPrototypeOf(this,Xo.prototype)}
w(Xo,Wo);function Yo(a,b){var c=Error.call(this);this.message=c.message;"stack"in c&&(this.stack=c.stack);this.index=a;this.objectStore=b;Object.setPrototypeOf(this,Yo.prototype)}
w(Yo,Error);var Zo=["The database connection is closing","Can't start a transaction on a closed database","A mutation operation was attempted on a database that did not allow mutations"];
function $o(a,b,c,d){b=No(b);var e=a instanceof Error?a:Error("Unexpected error: "+a);if(e instanceof Wo)return e;a={objectStoreNames:c,dbName:b,dbVersion:d};if(e.name==="QuotaExceededError")return new Wo("QUOTA_EXCEEDED",a);if(jd&&e.name==="UnknownError")return new Wo("QUOTA_MAYBE_EXCEEDED",a);if(e instanceof Yo)return new Wo("MISSING_INDEX",Object.assign({},a,{objectStore:e.objectStore,index:e.index}));if(e.name==="InvalidStateError"&&Zo.some(function(f){return e.message.includes(f)}))return new Wo("EXECUTE_TRANSACTION_ON_CLOSED_DB",
a);
if(e.name==="AbortError")return new Wo("UNKNOWN_ABORT",a,e.message);e.args=[Object.assign({},a,{name:"IdbError",Dd:e.name})];e.level="WARNING";return e}
function ap(a,b,c){var d=Co();return new Wo("IDB_NOT_SUPPORTED",{context:{caller:a,publicName:b,version:c,hasSucceededOnce:d==null?void 0:d.hasSucceededOnce}})}
;function bp(a){if(!a)throw Error();throw a;}
function cp(a){return a}
function dp(a){this.h=a}
function ep(a){function b(e){if(d.state.status==="PENDING"){d.state={status:"REJECTED",reason:e};e=y(d.i);for(var f=e.next();!f.done;f=e.next())f=f.value,f()}}
function c(e){if(d.state.status==="PENDING"){d.state={status:"FULFILLED",value:e};e=y(d.h);for(var f=e.next();!f.done;f=e.next())f=f.value,f()}}
var d=this;this.state={status:"PENDING"};this.h=[];this.i=[];a=a.h;try{a(c,b)}catch(e){b(e)}}
ep.all=function(a){return new ep(new dp(function(b,c){var d=[],e=a.length;e===0&&b(d);for(var f={zb:0};f.zb<a.length;f={zb:f.zb},++f.zb)ep.resolve(a[f.zb]).then(function(g){return function(h){d[g.zb]=h;e--;e===0&&b(d)}}(f)).catch(function(g){c(g)})}))};
ep.resolve=function(a){return new ep(new dp(function(b,c){a instanceof ep?a.then(b,c):b(a)}))};
ep.reject=function(a){return new ep(new dp(function(b,c){c(a)}))};
ep.prototype.then=function(a,b){var c=this,d=a!=null?a:cp,e=b!=null?b:bp;return new ep(new dp(function(f,g){c.state.status==="PENDING"?(c.h.push(function(){fp(c,c,d,f,g)}),c.i.push(function(){gp(c,c,e,f,g)})):c.state.status==="FULFILLED"?fp(c,c,d,f,g):c.state.status==="REJECTED"&&gp(c,c,e,f,g)}))};
ep.prototype.catch=function(a){return this.then(void 0,a)};
function fp(a,b,c,d,e){try{if(a.state.status!=="FULFILLED")throw Error("calling handleResolve before the promise is fulfilled.");var f=c(a.state.value);f instanceof ep?hp(a,b,f,d,e):d(f)}catch(g){e(g)}}
function gp(a,b,c,d,e){try{if(a.state.status!=="REJECTED")throw Error("calling handleReject before the promise is rejected.");var f=c(a.state.reason);f instanceof ep?hp(a,b,f,d,e):d(f)}catch(g){e(g)}}
function hp(a,b,c,d,e){b===c?e(new TypeError("Circular promise chain detected.")):c.then(function(f){f instanceof ep?hp(a,b,f,d,e):d(f)},function(f){e(f)})}
;function ip(a,b,c){function d(){c(a.error);f()}
function e(){b(a.result);f()}
function f(){try{a.removeEventListener("success",e),a.removeEventListener("error",d)}catch(g){}}
a.addEventListener("success",e);a.addEventListener("error",d)}
function jp(a){return new Promise(function(b,c){ip(a,b,c)})}
function kp(a){return new ep(new dp(function(b,c){ip(a,b,c)}))}
;function lp(a,b){return new ep(new dp(function(c,d){function e(){var f=a?b(a):null;f?f.then(function(g){a=g;e()},d):c()}
e()}))}
;var mp=window,U=mp.ytcsi&&mp.ytcsi.now?mp.ytcsi.now:mp.performance&&mp.performance.timing&&mp.performance.now&&mp.performance.timing.navigationStart?function(){return mp.performance.timing.navigationStart+mp.performance.now()}:function(){return(new Date).getTime()};function np(a,b){this.h=a;this.options=b;this.transactionCount=0;this.j=Math.round(U());this.i=!1}
r=np.prototype;r.add=function(a,b,c){return op(this,[a],{mode:"readwrite",ka:!0},function(d){return d.objectStore(a).add(b,c)})};
r.clear=function(a){return op(this,[a],{mode:"readwrite",ka:!0},function(b){return b.objectStore(a).clear()})};
r.close=function(){this.h.close();var a;((a=this.options)==null?0:a.closed)&&this.options.closed()};
r.count=function(a,b){return op(this,[a],{mode:"readonly",ka:!0},function(c){return c.objectStore(a).count(b)})};
function pp(a,b,c){a=a.h.createObjectStore(b,c);return new qp(a)}
r.delete=function(a,b){return op(this,[a],{mode:"readwrite",ka:!0},function(c){return c.objectStore(a).delete(b)})};
r.get=function(a,b){return op(this,[a],{mode:"readonly",ka:!0},function(c){return c.objectStore(a).get(b)})};
function rp(a,b,c){return op(a,[b],{mode:"readwrite",ka:!0},function(d){d=d.objectStore(b);return kp(d.h.put(c,void 0))})}
r.objectStoreNames=function(){return Array.from(this.h.objectStoreNames)};
function op(a,b,c,d){var e,f,g,h,k,l,m,n,p,t,u,x;return A(function(z){switch(z.h){case 1:var H={mode:"readonly",ka:!1,tag:"IDB_TRANSACTION_TAG_UNKNOWN"};typeof c==="string"?H.mode=c:Object.assign(H,c);e=H;a.transactionCount++;f=e.ka?3:1;g=0;case 2:if(h){z.A(4);break}g++;k=Math.round(U());za(z,5);l=a.h.transaction(b,e.mode);H=z.yield;var K=new sp(l);K=tp(K,d);return H.call(z,K,7);case 7:return m=z.i,n=Math.round(U()),up(a,k,n,g,void 0,b.join(),e),z.return(m);case 5:p=Ba(z);t=Math.round(U());u=$o(p,
a.h.name,b.join(),a.h.version);if((x=u instanceof Wo&&!u.h)||g>=f)up(a,k,t,g,u,b.join(),e),h=u;z.A(2);break;case 4:return z.return(Promise.reject(h))}})}
function up(a,b,c,d,e,f,g){b=c-b;e?(e instanceof Wo&&(e.type==="QUOTA_EXCEEDED"||e.type==="QUOTA_MAYBE_EXCEEDED")&&Lo("QUOTA_EXCEEDED",{dbName:No(a.h.name),objectStoreNames:f,transactionCount:a.transactionCount,transactionMode:g.mode}),e instanceof Wo&&e.type==="UNKNOWN_ABORT"&&(c-=a.j,c<0&&c>=2147483648&&(c=0),Lo("TRANSACTION_UNEXPECTEDLY_ABORTED",{objectStoreNames:f,transactionDuration:b,transactionCount:a.transactionCount,dbDuration:c}),a.i=!0),vp(a,!1,d,f,b,g.tag),Ko(e)):vp(a,!0,d,f,b,g.tag)}
function vp(a,b,c,d,e,f){Lo("TRANSACTION_ENDED",{objectStoreNames:d,connectionHasUnknownAbortedTransaction:a.i,duration:e,isSuccessful:b,tryCount:c,tag:f===void 0?"IDB_TRANSACTION_TAG_UNKNOWN":f})}
r.getName=function(){return this.h.name};
function qp(a){this.h=a}
r=qp.prototype;r.add=function(a,b){return kp(this.h.add(a,b))};
r.autoIncrement=function(){return this.h.autoIncrement};
r.clear=function(){return kp(this.h.clear()).then(function(){})};
function wp(a,b,c){a.h.createIndex(b,c,{unique:!1})}
r.count=function(a){return kp(this.h.count(a))};
function xp(a,b){return yp(a,{query:b},function(c){return c.delete().then(function(){return zp(c)})}).then(function(){})}
r.delete=function(a){return a instanceof IDBKeyRange?xp(this,a):kp(this.h.delete(a))};
r.get=function(a){return kp(this.h.get(a))};
r.index=function(a){try{return new Ap(this.h.index(a))}catch(b){if(b instanceof Error&&b.name==="NotFoundError")throw new Yo(a,this.h.name);throw b;}};
r.getName=function(){return this.h.name};
r.keyPath=function(){return this.h.keyPath};
function yp(a,b,c){a=a.h.openCursor(b.query,b.direction);return Bp(a).then(function(d){return lp(d,c)})}
function sp(a){var b=this;this.h=a;this.i=new Map;this.aborted=!1;this.done=new Promise(function(c,d){b.h.addEventListener("complete",function(){c()});
b.h.addEventListener("error",function(e){e.currentTarget===e.target&&d(b.h.error)});
b.h.addEventListener("abort",function(){var e=b.h.error;if(e)d(e);else if(!b.aborted){e=Wo;for(var f=b.h.objectStoreNames,g=[],h=0;h<f.length;h++){var k=f.item(h);if(k===null)throw Error("Invariant: item in DOMStringList is null");g.push(k)}e=new e("UNKNOWN_ABORT",{objectStoreNames:g.join(),dbName:b.h.db.name,mode:b.h.mode});d(e)}})})}
function tp(a,b){var c=new Promise(function(d,e){try{b(a).then(function(f){d(f)}).catch(e)}catch(f){e(f),a.abort()}});
return Promise.all([c,a.done]).then(function(d){return y(d).next().value})}
sp.prototype.abort=function(){this.h.abort();this.aborted=!0;throw new Wo("EXPLICIT_ABORT");};
sp.prototype.objectStore=function(a){a=this.h.objectStore(a);var b=this.i.get(a);b||(b=new qp(a),this.i.set(a,b));return b};
function Ap(a){this.h=a}
r=Ap.prototype;r.count=function(a){return kp(this.h.count(a))};
r.delete=function(a){return Cp(this,{query:a},function(b){return b.delete().then(function(){return zp(b)})})};
r.get=function(a){return kp(this.h.get(a))};
r.keyPath=function(){return this.h.keyPath};
r.unique=function(){return this.h.unique};
function Cp(a,b,c){a=a.h.openCursor(b.query===void 0?null:b.query,b.direction===void 0?"next":b.direction);return Bp(a).then(function(d){return lp(d,c)})}
function Dp(a,b){this.request=a;this.cursor=b}
function Bp(a){return kp(a).then(function(b){return b?new Dp(a,b):null})}
function zp(a){a.cursor.continue(void 0);return Bp(a.request)}
Dp.prototype.delete=function(){return kp(this.cursor.delete()).then(function(){})};
Dp.prototype.getValue=function(){return this.cursor.value};
Dp.prototype.update=function(a){return kp(this.cursor.update(a))};function Ep(a,b,c){return new Promise(function(d,e){function f(){p||(p=new np(g.result,{closed:n}));return p}
var g=b!==void 0?self.indexedDB.open(a,b):self.indexedDB.open(a);var h=c.ce,k=c.blocking,l=c.tf,m=c.upgrade,n=c.closed,p;g.addEventListener("upgradeneeded",function(t){try{if(t.newVersion===null)throw Error("Invariant: newVersion on IDbVersionChangeEvent is null");if(g.transaction===null)throw Error("Invariant: transaction on IDbOpenDbRequest is null");t.dataLoss&&t.dataLoss!=="none"&&Lo("IDB_DATA_CORRUPTED",{reason:t.dataLossMessage||"unknown reason",dbName:No(a)});var u=f(),x=new sp(g.transaction);
m&&m(u,function(z){return t.oldVersion<z&&t.newVersion>=z},x);
x.done.catch(function(z){e(z)})}catch(z){e(z)}});
g.addEventListener("success",function(){var t=g.result;k&&t.addEventListener("versionchange",function(){k(f())});
t.addEventListener("close",function(){Lo("IDB_UNEXPECTEDLY_CLOSED",{dbName:No(a),dbVersion:t.version});l&&l()});
d(f())});
g.addEventListener("error",function(){e(g.error)});
h&&g.addEventListener("blocked",function(){h()})})}
function Fp(a,b,c){c=c===void 0?{}:c;return Ep(a,b,c)}
function Gp(a,b){b=b===void 0?{}:b;var c,d,e,f;return A(function(g){if(g.h==1)return za(g,2),c=self.indexedDB.deleteDatabase(a),d=b,(e=d.ce)&&c.addEventListener("blocked",function(){e()}),g.yield(jp(c),4);
if(g.h!=2)return Aa(g,0);f=Ba(g);throw $o(f,a,"",-1);})}
;function Hp(a,b){this.name=a;this.options=b;this.j=!0;this.u=this.o=0}
Hp.prototype.i=function(a,b,c){c=c===void 0?{}:c;return Fp(a,b,c)};
Hp.prototype.delete=function(a){a=a===void 0?{}:a;return Gp(this.name,a)};
function Ip(a,b){return new Wo("INCOMPATIBLE_DB_VERSION",{dbName:a.name,oldVersion:a.options.version,newVersion:b})}
function Jp(a,b){if(!b)throw ap("openWithToken",No(a.name));return a.open()}
Hp.prototype.open=function(){function a(){var f,g,h,k,l,m,n,p,t,u;return A(function(x){switch(x.h){case 1:return g=(f=Error().stack)!=null?f:"",za(x,2),x.yield(c.i(c.name,c.options.version,e),4);case 4:for(var z=h=x.i,H=c.options,K=[],da=y(Object.keys(H.Fb)),ea=da.next();!ea.done;ea=da.next()){ea=ea.value;var Oa=H.Fb[ea],Ob=Oa.Xe===void 0?Number.MAX_VALUE:Oa.Xe;!(z.h.version>=Oa.Lb)||z.h.version>=Ob||z.h.objectStoreNames.contains(ea)||K.push(ea)}k=K;if(k.length===0){x.A(5);break}l=Object.keys(c.options.Fb);
m=h.objectStoreNames();if(c.u<S("ytidb_reopen_db_retries",0))return c.u++,h.close(),Ko(new Wo("DB_REOPENED_BY_MISSING_OBJECT_STORES",{dbName:c.name,expectedObjectStores:l,foundObjectStores:m})),x.return(a());if(!(c.o<S("ytidb_remake_db_retries",1))){x.A(6);break}c.o++;return x.yield(c.delete(),7);case 7:return Ko(new Wo("DB_DELETED_BY_MISSING_OBJECT_STORES",{dbName:c.name,expectedObjectStores:l,foundObjectStores:m})),x.return(a());case 6:throw new Xo(m,l);case 5:return x.return(h);case 2:n=Ba(x);
if(n instanceof DOMException?n.name!=="VersionError":"DOMError"in self&&n instanceof DOMError?n.name!=="VersionError":!(n instanceof Object&&"message"in n)||n.message!=="An attempt was made to open a database using a lower version than the existing version."){x.A(8);break}return x.yield(c.i(c.name,void 0,Object.assign({},e,{upgrade:void 0})),9);case 9:p=x.i;t=p.h.version;if(c.options.version!==void 0&&t>c.options.version+1)throw p.close(),c.j=!1,Ip(c,t);return x.return(p);case 8:throw b(),n instanceof
Error&&!R("ytidb_async_stack_killswitch")&&(n.stack=n.stack+"\n"+g.substring(g.indexOf("\n")+1)),$o(n,c.name,"",(u=c.options.version)!=null?u:-1);}})}
function b(){c.h===d&&(c.h=void 0)}
var c=this;if(!this.j)throw Ip(this);if(this.h)return this.h;var d,e={blocking:function(f){f.close()},
closed:b,tf:b,upgrade:this.options.upgrade};return this.h=d=a()};var Kp=new Hp("YtIdbMeta",{Fb:{databases:{Lb:1}},upgrade:function(a,b){b(1)&&pp(a,"databases",{keyPath:"actualName"})}});
function Lp(a,b){var c;return A(function(d){if(d.h==1)return d.yield(Jp(Kp,b),2);c=d.i;return d.return(op(c,["databases"],{ka:!0,mode:"readwrite"},function(e){var f=e.objectStore("databases");return f.get(a.actualName).then(function(g){if(g?a.actualName!==g.actualName||a.publicName!==g.publicName||a.userIdentifier!==g.userIdentifier:1)return kp(f.h.put(a,void 0)).then(function(){})})}))})}
function Mp(a,b){var c;return A(function(d){if(d.h==1)return a?d.yield(Jp(Kp,b),2):d.return();c=d.i;return d.return(c.delete("databases",a))})}
function Np(a,b){var c,d;return A(function(e){return e.h==1?(c=[],e.yield(Jp(Kp,b),2)):e.h!=3?(d=e.i,e.yield(op(d,["databases"],{ka:!0,mode:"readonly"},function(f){c.length=0;return yp(f.objectStore("databases"),{},function(g){a(g.getValue())&&c.push(g.getValue());return zp(g)})}),3)):e.return(c)})}
function Op(a){return Np(function(b){return b.publicName==="LogsDatabaseV2"&&b.userIdentifier!==void 0},a)}
function Pp(a,b,c){return Np(function(d){return c?d.userIdentifier!==void 0&&!a.includes(d.userIdentifier)&&c.includes(d.publicName):d.userIdentifier!==void 0&&!a.includes(d.userIdentifier)},b)}
function Qp(a){var b,c;return A(function(d){if(d.h==1)return b=Sn("YtIdbMeta hasAnyMeta other"),d.yield(Np(function(e){return e.userIdentifier!==void 0&&e.userIdentifier!==b},a),2);
c=d.i;return d.return(c.length>0)})}
;var Rp,Sp=new function(){}(new function(){});
function Tp(){var a,b,c,d;return A(function(e){switch(e.h){case 1:a=Co();if((b=a)==null?0:b.hasSucceededOnce)return e.return(!0);var f;if(f=Oo)f=/WebKit\/([0-9]+)/.exec(Sc()),f=!!(f&&parseInt(f[1],10)>=600);f&&(f=/WebKit\/([0-9]+)/.exec(Sc()),f=!(f&&parseInt(f[1],10)>=602));if(f||dd)return e.return(!1);try{if(c=self,!(c.indexedDB&&c.IDBIndex&&c.IDBKeyRange&&c.IDBObjectStore))return e.return(!1)}catch(g){return e.return(!1)}if(!("IDBTransaction"in self&&"objectStoreNames"in IDBTransaction.prototype))return e.return(!1);
za(e,2);d={actualName:"yt-idb-test-do-not-use",publicName:"yt-idb-test-do-not-use",userIdentifier:void 0};return e.yield(Lp(d,Sp),4);case 4:return e.yield(Mp("yt-idb-test-do-not-use",Sp),5);case 5:return e.return(!0);case 2:return Ba(e),e.return(!1)}})}
function Up(){if(Rp!==void 0)return Rp;Fo=!0;return Rp=Tp().then(function(a){Fo=!1;var b;if((b=Bo())!=null&&b.h){var c;b={hasSucceededOnce:((c=Co())==null?void 0:c.hasSucceededOnce)||a};var d;(d=Bo())==null||d.set("LAST_RESULT_ENTRY_KEY",b,2592E3,!0)}return a})}
function Vp(){return E("ytglobal.idbToken_")||void 0}
function Wp(){var a=Vp();return a?Promise.resolve(a):Up().then(function(b){(b=b?Sp:void 0)&&D("ytglobal.idbToken_",b);return b})}
;var Xp=0;function Yp(a,b){Xp||(Xp=ak.ma(function(){var c,d,e,f,g;return A(function(h){switch(h.h){case 1:return h.yield(Wp(),2);case 2:c=h.i;if(!c)return h.return();d=!0;za(h,3);return h.yield(Pp(a,c,b),5);case 5:e=h.i;if(!e.length){d=!1;h.A(6);break}f=e[0];return h.yield(Gp(f.actualName),7);case 7:return h.yield(Mp(f.actualName,c),6);case 6:Aa(h,4);break;case 3:g=Ba(h),Ko(g),d=!1;case 4:ak.qa(Xp),Xp=0,d&&Yp(a,b),h.h=0}})}))}
function Zp(){var a;return A(function(b){return b.h==1?b.yield(Wp(),2):(a=b.i)?b.return(Qp(a)):b.return(!1)})}
new yj;function $p(a){if(!Rn())throw a=new Wo("AUTH_INVALID",{dbName:a}),Ko(a),a;var b=Sn();return{actualName:a+":"+b,publicName:a,userIdentifier:b}}
function aq(a,b,c,d){var e,f,g,h,k,l;return A(function(m){switch(m.h){case 1:return f=(e=Error().stack)!=null?e:"",m.yield(Wp(),2);case 2:g=m.i;if(!g)throw h=ap("openDbImpl",a,b),R("ytidb_async_stack_killswitch")||(h.stack=h.stack+"\n"+f.substring(f.indexOf("\n")+1)),Ko(h),h;Mo(a);k=c?{actualName:a,publicName:a,userIdentifier:void 0}:$p(a);za(m,3);return m.yield(Lp(k,g),5);case 5:return m.yield(Fp(k.actualName,b,d),6);case 6:return m.return(m.i);case 3:return l=Ba(m),za(m,7),m.yield(Mp(k.actualName,
g),9);case 9:Aa(m,8);break;case 7:Ba(m);case 8:throw l;}})}
function bq(a,b,c){c=c===void 0?{}:c;return aq(a,b,!1,c)}
function cq(a,b,c){c=c===void 0?{}:c;return aq(a,b,!0,c)}
function dq(a,b){b=b===void 0?{}:b;var c,d;return A(function(e){if(e.h==1)return e.yield(Wp(),2);if(e.h!=3){c=e.i;if(!c)return e.return();Mo(a);d=$p(a);return e.yield(Gp(d.actualName,b),3)}return e.yield(Mp(d.actualName,c),0)})}
function eq(a,b,c){a=a.map(function(d){return A(function(e){return e.h==1?e.yield(Gp(d.actualName,b),2):e.yield(Mp(d.actualName,c),0)})});
return Promise.all(a).then(function(){})}
function fq(){var a=a===void 0?{}:a;var b,c;return A(function(d){if(d.h==1)return d.yield(Wp(),2);if(d.h!=3){b=d.i;if(!b)return d.return();Mo("LogsDatabaseV2");return d.yield(Op(b),3)}c=d.i;return d.yield(eq(c,a,b),0)})}
function gq(a,b){b=b===void 0?{}:b;var c;return A(function(d){if(d.h==1)return d.yield(Wp(),2);if(d.h!=3){c=d.i;if(!c)return d.return();Mo(a);return d.yield(Gp(a,b),3)}return d.yield(Mp(a,c),0)})}
;function hq(a,b){Hp.call(this,a,b);this.options=b;Mo(a)}
w(hq,Hp);function iq(a,b){var c;return function(){c||(c=new hq(a,b));return c}}
hq.prototype.i=function(a,b,c){c=c===void 0?{}:c;return(this.options.shared?cq:bq)(a,b,Object.assign({},c))};
hq.prototype.delete=function(a){a=a===void 0?{}:a;return(this.options.shared?gq:dq)(this.name,a)};
function jq(a,b){return iq(a,b)}
;var kq={},lq=jq("ytGcfConfig",{Fb:(kq.coldConfigStore={Lb:1},kq.hotConfigStore={Lb:1},kq),shared:!1,upgrade:function(a,b){b(1)&&(wp(pp(a,"hotConfigStore",{keyPath:"key",autoIncrement:!0}),"hotTimestampIndex","timestamp"),wp(pp(a,"coldConfigStore",{keyPath:"key",autoIncrement:!0}),"coldTimestampIndex","timestamp"))},
version:1});function mq(a){return Jp(lq(),a)}
function nq(a,b,c){var d,e,f;return A(function(g){switch(g.h){case 1:return d={config:a,hashData:b,timestamp:U()},g.yield(mq(c),2);case 2:return e=g.i,g.yield(e.clear("hotConfigStore"),3);case 3:return g.yield(rp(e,"hotConfigStore",d),4);case 4:return f=g.i,g.return(f)}})}
function oq(a,b,c,d){var e,f,g;return A(function(h){switch(h.h){case 1:return e={config:a,hashData:b,configData:c,timestamp:U()},h.yield(mq(d),2);case 2:return f=h.i,h.yield(f.clear("coldConfigStore"),3);case 3:return h.yield(rp(f,"coldConfigStore",e),4);case 4:return g=h.i,h.return(g)}})}
function pq(a){var b,c;return A(function(d){return d.h==1?d.yield(mq(a),2):d.h!=3?(b=d.i,c=void 0,d.yield(op(b,["coldConfigStore"],{mode:"readwrite",ka:!0},function(e){return Cp(e.objectStore("coldConfigStore").index("coldTimestampIndex"),{direction:"prev"},function(f){c=f.getValue()})}),3)):d.return(c)})}
function qq(a){var b,c;return A(function(d){return d.h==1?d.yield(mq(a),2):d.h!=3?(b=d.i,c=void 0,d.yield(op(b,["hotConfigStore"],{mode:"readwrite",ka:!0},function(e){return Cp(e.objectStore("hotConfigStore").index("hotTimestampIndex"),{direction:"prev"},function(f){c=f.getValue()})}),3)):d.return(c)})}
;function rq(){F.call(this);this.i=[];this.h=[];var a=E("yt.gcf.config.hotUpdateCallbacks");a?(this.i=[].concat(ra(a)),this.h=a):(this.h=[],D("yt.gcf.config.hotUpdateCallbacks",this.h))}
w(rq,F);rq.prototype.ba=function(){for(var a=y(this.i),b=a.next();!b.done;b=a.next()){var c=this.h;b=c.indexOf(b.value);b>=0&&c.splice(b,1)}this.i.length=0;F.prototype.ba.call(this)};function sq(){this.h=0;this.i=new rq}
function tq(){var a;return(a=E("yt.gcf.config.hotConfigGroup"))!=null?a:P("RAW_HOT_CONFIG_GROUP")}
function uq(a,b,c){var d,e,f;return A(function(g){switch(g.h){case 1:if(!R("start_client_gcf")){g.A(0);break}c&&(a.j=c,D("yt.gcf.config.hotConfigGroup",a.j||null));a.o(b);d=Vp();if(!d){g.A(3);break}if(c){g.A(4);break}return g.yield(qq(d),5);case 5:e=g.i,c=(f=e)==null?void 0:f.config;case 4:return g.yield(nq(c,b,d),3);case 3:if(c)for(var h=c,k=y(a.i.h),l=k.next();!l.done;l=k.next())l=l.value,l(h);g.h=0}})}
function vq(a,b,c){var d,e,f,g;return A(function(h){if(h.h==1){if(!R("start_client_gcf"))return h.A(0);a.coldHashData=b;D("yt.gcf.config.coldHashData",a.coldHashData||null);return(d=Vp())?c?h.A(4):h.yield(pq(d),5):h.A(0)}h.h!=4&&(e=h.i,c=(f=e)==null?void 0:f.config);if(!c)return h.A(0);g=c.configData;return h.yield(oq(c,b,g,d),0)})}
function wq(){if(!sq.h){var a=new sq;sq.h=a}a=sq.h;var b=U()-a.h;if(!(a.h!==0&&b<S("send_config_hash_timer"))){b=E("yt.gcf.config.coldConfigData");var c=E("yt.gcf.config.hotHashData"),d=E("yt.gcf.config.coldHashData");b&&c&&d&&(a.h=U());return{coldConfigData:b,hotHashData:c,coldHashData:d}}}
sq.prototype.o=function(a){this.hotHashData=a;D("yt.gcf.config.hotHashData",this.hotHashData||null)};function xq(){return"INNERTUBE_API_KEY"in pm&&"INNERTUBE_API_VERSION"in pm}
function yq(){return{innertubeApiKey:P("INNERTUBE_API_KEY"),innertubeApiVersion:P("INNERTUBE_API_VERSION"),Be:P("INNERTUBE_CONTEXT_CLIENT_CONFIG_INFO"),xd:P("INNERTUBE_CONTEXT_CLIENT_NAME","WEB"),Dh:P("INNERTUBE_CONTEXT_CLIENT_NAME",1),innertubeContextClientVersion:P("INNERTUBE_CONTEXT_CLIENT_VERSION"),De:P("INNERTUBE_CONTEXT_HL"),Ce:P("INNERTUBE_CONTEXT_GL"),Ee:P("INNERTUBE_HOST_OVERRIDE")||"",Fe:!!P("INNERTUBE_USE_THIRD_PARTY_AUTH",!1),Eh:!!P("INNERTUBE_OMIT_API_KEY_WHEN_AUTH_HEADER_IS_PRESENT",
!1),appInstallData:P("SERIALIZED_CLIENT_CONFIG_DATA")}}
function zq(a){var b={client:{hl:a.De,gl:a.Ce,clientName:a.xd,clientVersion:a.innertubeContextClientVersion,configInfo:a.Be}};navigator.userAgent&&(b.client.userAgent=String(navigator.userAgent));var c=C.devicePixelRatio;c&&c!=1&&(b.client.screenDensityFloat=String(c));c=P("EXPERIMENTS_TOKEN","");c!==""&&(b.client.experimentsToken=c);c=Rm();c.length>0&&(b.request={internalExperimentFlags:c});c=a.xd;if((c==="WEB"||c==="MWEB"||c===1||c===2)&&b){var d;b.client.mainAppWebInfo=(d=b.client.mainAppWebInfo)!=
null?d:{};b.client.mainAppWebInfo.webDisplayMode=vn()}(d=E("yt.embedded_player.embed_url"))&&b&&(b.thirdParty={embedUrl:d});var e;if(R("web_log_memory_total_kbytes")&&((e=C.navigator)==null?0:e.deviceMemory)){var f;e=(f=C.navigator)==null?void 0:f.deviceMemory;b&&(b.client.memoryTotalKbytes=""+e*1E6)}a.appInstallData&&b&&(b.client.configInfo=b.client.configInfo||{},b.client.configInfo.appInstallData=a.appInstallData);(a=Pn())&&b&&(b.client.connectionType=a);R("web_log_effective_connection_type")&&
(a=Qn())&&b&&(b.client.effectiveConnectionType=a);R("start_client_gcf")&&(e=wq())&&(a=e.coldConfigData,f=e.coldHashData,e=e.hotHashData,b&&(b.client.configInfo=b.client.configInfo||{},a&&(b.client.configInfo.coldConfigData=a),f&&(b.client.configInfo.coldHashData=f),e&&(b.client.configInfo.hotHashData=e)));P("DELEGATED_SESSION_ID")&&!R("pageid_as_header_web")&&(b.user={onBehalfOfUser:P("DELEGATED_SESSION_ID")});!R("fill_delegate_context_in_gel_killswitch")&&(a=P("INNERTUBE_CONTEXT_SERIALIZED_DELEGATION_CONTEXT"))&&
(b.user=Object.assign({},b.user,{serializedDelegationContext:a}));a=P("INNERTUBE_CONTEXT");var g;if(R("enable_persistent_device_token")&&(a==null?0:(g=a.client)==null?0:g.rolloutToken)){var h;b.client.rolloutToken=a==null?void 0:(h=a.client)==null?void 0:h.rolloutToken}g=Object;h=g.assign;a=b.client;f={};e=y(Object.entries(Em(P("DEVICE",""))));for(d=e.next();!d.done;d=e.next())c=y(d.value),d=c.next().value,c=c.next().value,d==="cbrand"?f.deviceMake=c:d==="cmodel"?f.deviceModel=c:d==="cbr"?f.browserName=
c:d==="cbrver"?f.browserVersion=c:d==="cos"?f.osName=c:d==="cosver"?f.osVersion=c:d==="cplatform"&&(f.platform=c);b.client=h.call(g,a,f);return b}
function Aq(a,b,c){c=c===void 0?{}:c;var d={};P("EOM_VISITOR_DATA")?d={"X-Goog-EOM-Visitor-Id":P("EOM_VISITOR_DATA")}:d={"X-Goog-Visitor-Id":c.visitorData||P("VISITOR_DATA","")};if(b&&b.includes("www.youtube-nocookie.com"))return d;b=c.authorization||P("AUTHORIZATION");b||(a?b="Bearer "+E("gapi.auth.getToken")().th:(a=yn(xn()),R("pageid_as_header_web")||delete a["X-Goog-PageId"],d=Object.assign({},d,a)));b&&(d.Authorization=b);return d}
;var Bq=typeof TextEncoder!=="undefined"?new TextEncoder:null,Cq=Bq?function(a){return Bq.encode(a)}:function(a){for(var b=[],c=0,d=0;d<a.length;d++){var e=a.charCodeAt(d);
e<128?b[c++]=e:(e<2048?b[c++]=e>>6|192:((e&64512)==55296&&d+1<a.length&&(a.charCodeAt(d+1)&64512)==56320?(e=65536+((e&1023)<<10)+(a.charCodeAt(++d)&1023),b[c++]=e>>18|240,b[c++]=e>>12&63|128):b[c++]=e>>12|224,b[c++]=e>>6&63|128),b[c++]=e&63|128)}a=new Uint8Array(b.length);for(c=0;c<a.length;c++)a[c]=b[c];return a};var Dq={next:"wn_s",browse:"br_s",search:"sr_s",reel:"r_wrs",player:"ps_s"},Eq={next:"wn_r",browse:"br_r",search:"sr_r",reel:"r_wrr",player:"ps_r"};function Fq(a,b){this.version=a;this.args=b}
Fq.prototype.serialize=function(){return{version:this.version,args:this.args}};function Gq(a,b){this.topic=a;this.h=b}
Gq.prototype.toString=function(){return this.topic};var Hq=E("ytPubsub2Pubsub2Instance")||new M;M.prototype.subscribe=M.prototype.subscribe;M.prototype.unsubscribeByKey=M.prototype.ac;M.prototype.publish=M.prototype.qb;M.prototype.clear=M.prototype.clear;D("ytPubsub2Pubsub2Instance",Hq);var Iq=E("ytPubsub2Pubsub2SubscribedKeys")||{};D("ytPubsub2Pubsub2SubscribedKeys",Iq);var Jq=E("ytPubsub2Pubsub2TopicToKeys")||{};D("ytPubsub2Pubsub2TopicToKeys",Jq);var Kq=E("ytPubsub2Pubsub2IsAsync")||{};D("ytPubsub2Pubsub2IsAsync",Kq);
D("ytPubsub2Pubsub2SkipSubKey",null);function Lq(a,b){var c=Mq();c&&c.publish.call(c,a.toString(),a,b)}
function Nq(a){var b=Oq,c=Mq();if(!c)return 0;var d=c.subscribe(b.toString(),function(e,f){var g=E("ytPubsub2Pubsub2SkipSubKey");g&&g==d||(g=function(){if(Iq[d])try{if(f&&b instanceof Gq&&b!=e)try{var h=b.h,k=f;if(!k.args||!k.version)throw Error("yt.pubsub2.Data.deserialize(): serializedData is incomplete.");try{if(!h.Rd){var l=new h;h.Rd=l.version}var m=h.Rd}catch(z){}if(!m||k.version!=m)throw Error("yt.pubsub2.Data.deserialize(): serializedData version is incompatible.");try{m=Reflect;var n=m.construct;
var p=k.args,t=p.length;if(t>0){var u=Array(t);for(k=0;k<t;k++)u[k]=p[k];var x=u}else x=[];f=n.call(m,h,x)}catch(z){throw z.message="yt.pubsub2.Data.deserialize(): "+z.message,z;}}catch(z){throw z.message="yt.pubsub2.pubsub2 cross-binary conversion error for "+b.toString()+": "+z.message,z;}a.call(window,f)}catch(z){wm(z)}},Kq[b.toString()]?E("yt.scheduler.instance")?ak.ma(g):Om(g,0):g())});
Iq[d]=!0;Jq[b.toString()]||(Jq[b.toString()]=[]);Jq[b.toString()].push(d);return d}
function Pq(){var a=Qq,b=Nq(function(c){a.apply(void 0,arguments);Rq(b)});
return b}
function Rq(a){var b=Mq();b&&(typeof a==="number"&&(a=[a]),Sb(a,function(c){b.unsubscribeByKey(c);delete Iq[c]}))}
function Mq(){return E("ytPubsub2Pubsub2Instance")}
;function Sq(a,b,c){c=c===void 0?{sampleRate:.1}:c;Math.random()<Math.min(.02,c.sampleRate/100)&&Lq("meta_logging_csi_event",{timerName:a,Wh:b})}
;var Tq=void 0,Uq=void 0;function Vq(){Uq||(Uq=Pl(P("WORKER_SERIALIZATION_URL")));return Uq||void 0}
function Wq(){var a=Vq();Tq||a===void 0||(Tq=new Worker(pb(a),void 0));return Tq}
;var Xq=S("max_body_size_to_compress",5E5),Yq=S("min_body_size_to_compress",500),Zq=!0,$q=0,ar=0,br=S("compression_performance_threshold_lr",250),cr=S("slow_compressions_before_abandon_count",4),dr=!1,er=new Map,fr=1,gr=!0;function hr(){if(typeof Worker==="function"&&Vq()&&!dr){var a=function(c){c=c.data;if(c.op==="gzippedGelBatch"){var d=er.get(c.key);d&&(ir(c.gzippedBatch,d.latencyPayload,d.url,d.options,d.sendFn),er.delete(c.key))}},b=Wq();
b&&(b.addEventListener("message",a),b.onerror=function(){er.clear()},dr=!0)}}
function jr(a,b,c,d,e){e=e===void 0?!1:e;var f={startTime:U(),ticks:{},infos:{}};if(Zq)try{var g=kr(b);if(g!=null&&(g>Xq||g<Yq))d(a,c);else{if(R("gzip_gel_with_worker")&&(R("initial_gzip_use_main_thread")&&!gr||!R("initial_gzip_use_main_thread"))){dr||hr();var h=Wq();if(h&&!e){er.set(fr,{latencyPayload:f,url:a,options:c,sendFn:d});h.postMessage({op:"gelBatchToGzip",serializedBatch:b,key:fr});fr++;return}}var k=Nl(Cq(b));ir(k,f,a,c,d)}}catch(l){xm(l),d(a,c)}else d(a,c)}
function ir(a,b,c,d,e){gr=!1;var f=U();b.ticks.gelc=f;ar++;R("disable_compression_due_to_performance_degredation")&&f-b.startTime>=br&&($q++,R("abandon_compression_after_N_slow_zips")?ar===S("compression_disable_point")&&$q>cr&&(Zq=!1):Zq=!1);lr(b);d.headers||(d.headers={});d.headers["Content-Encoding"]="gzip";d.postBody=a;d.postParams=void 0;e(c,d)}
function mr(a){var b=b===void 0?!1:b;var c=c===void 0?!1:c;var d=U(),e={startTime:d,ticks:{},infos:{}},f=b?E("yt.logging.gzipForFetch",!1):!0;if(Zq&&f){if(!a.body)return a;try{var g=c?a.body:typeof a.body==="string"?a.body:JSON.stringify(a.body);f=g;if(!c&&typeof g==="string"){var h=kr(g);if(h!=null&&(h>Xq||h<Yq))return a;c=b?{level:1}:void 0;f=Nl(Cq(g),c);var k=U();e.ticks.gelc=k;if(b){ar++;if((R("disable_compression_due_to_performance_degredation")||R("disable_compression_due_to_performance_degradation_lr"))&&
k-d>=br)if($q++,R("abandon_compression_after_N_slow_zips")||R("abandon_compression_after_N_slow_zips_lr")){b=$q/ar;var l=cr/S("compression_disable_point");ar>0&&ar%S("compression_disable_point")===0&&b>=l&&(Zq=!1)}else Zq=!1;lr(e)}}a.headers=Object.assign({},{"Content-Encoding":"gzip"},a.headers||{});a.body=f;return a}catch(m){return xm(m),a}}else return a}
function kr(a){try{return(new Blob(a.split(""))).size}catch(b){return xm(b),null}}
function lr(a){R("gel_compression_csi_killswitch")||!R("log_gel_compression_latency")&&!R("log_gel_compression_latency_lr")||Sq("gel_compression",a,{sampleRate:.1})}
;function nr(a){a=Object.assign({},a);delete a.Authorization;var b=lg();if(b){var c=new ek;c.update(P("INNERTUBE_API_KEY"));c.update(b);a.hash=md(c.digest(),3)}return a}
;var or;function pr(){or||(or=new Ao("yt.innertube"));return or}
function qr(a,b,c,d){if(d)return null;d=pr().get("nextId",!0)||1;var e=pr().get("requests",!0)||{};e[d]={method:a,request:b,authState:nr(c),requestTime:Math.round(U())};pr().set("nextId",d+1,86400,!0);pr().set("requests",e,86400,!0);return d}
function rr(a){var b=pr().get("requests",!0)||{};delete b[a];pr().set("requests",b,86400,!0)}
function sr(a){var b=pr().get("requests",!0);if(b){for(var c in b){var d=b[c];if(!(Math.round(U())-d.requestTime<6E4)){var e=d.authState,f=nr(Aq(!1));zg(e,f)&&(e=d.request,"requestTimeMs"in e&&(e.requestTimeMs=Math.round(U())),tr(a,d.method,e,{}));delete b[c]}}pr().set("requests",b,86400,!0)}}
;function ur(a){this.dc=this.h=!1;this.potentialEsfErrorCounter=this.i=0;this.handleError=function(){};
this.xb=function(){};
this.now=Date.now;this.Pb=!1;var b;this.Nd=(b=a.Nd)!=null?b:100;var c;this.Id=(c=a.Id)!=null?c:1;var d;this.Gd=(d=a.Gd)!=null?d:2592E6;var e;this.Fd=(e=a.Fd)!=null?e:12E4;var f;this.Hd=(f=a.Hd)!=null?f:5E3;var g;this.V=(g=a.V)!=null?g:void 0;this.jc=!!a.jc;var h;this.hc=(h=a.hc)!=null?h:.1;var k;this.xc=(k=a.xc)!=null?k:10;a.handleError&&(this.handleError=a.handleError);a.xb&&(this.xb=a.xb);a.Pb&&(this.Pb=a.Pb);a.dc&&(this.dc=a.dc);this.W=a.W;this.Ca=a.Ca;this.ga=a.ga;this.fa=a.fa;this.sendFn=a.sendFn;
this.Xc=a.Xc;this.Uc=a.Uc;vr(this)&&(!this.W||this.W("networkless_logging"))&&wr(this)}
function wr(a){vr(a)&&!a.Pb&&(a.h=!0,a.jc&&Math.random()<=a.hc&&a.ga.ee(a.V),xr(a),a.fa.ta()&&a.Zb(),a.fa.listen(a.Xc,a.Zb.bind(a)),a.fa.listen(a.Uc,a.pd.bind(a)))}
r=ur.prototype;r.writeThenSend=function(a,b){var c=this;b=b===void 0?{}:b;if(vr(this)&&this.h){var d={url:a,options:b,timestamp:this.now(),status:"NEW",sendCount:0};this.ga.set(d,this.V).then(function(e){d.id=e;c.fa.ta()&&yr(c,d)}).catch(function(e){yr(c,d);
zr(c,e)})}else this.sendFn(a,b)};
r.sendThenWrite=function(a,b,c){var d=this;b=b===void 0?{}:b;if(vr(this)&&this.h){var e={url:a,options:b,timestamp:this.now(),status:"NEW",sendCount:0};this.W&&this.W("nwl_skip_retry")&&(e.skipRetry=c);if(this.fa.ta()||this.W&&this.W("nwl_aggressive_send_then_write")&&!e.skipRetry){if(!e.skipRetry){var f=b.onError?b.onError:function(){};
b.onError=function(g,h){return A(function(k){if(k.h==1)return k.yield(d.ga.set(e,d.V).catch(function(l){zr(d,l)}),2);
f(g,h);k.h=0})}}this.sendFn(a,b,e.skipRetry)}else this.ga.set(e,this.V).catch(function(g){d.sendFn(a,b,e.skipRetry);
zr(d,g)})}else this.sendFn(a,b,this.W&&this.W("nwl_skip_retry")&&c)};
r.sendAndWrite=function(a,b){var c=this;b=b===void 0?{}:b;if(vr(this)&&this.h){var d={url:a,options:b,timestamp:this.now(),status:"NEW",sendCount:0},e=!1,f=b.onSuccess?b.onSuccess:function(){};
d.options.onSuccess=function(g,h){d.id!==void 0?c.ga.wb(d.id,c.V):e=!0;c.fa.kb&&c.W&&c.W("vss_network_hint")&&c.fa.kb(!0);f(g,h)};
this.sendFn(d.url,d.options,void 0,!0);this.ga.set(d,this.V).then(function(g){d.id=g;e&&c.ga.wb(d.id,c.V)}).catch(function(g){zr(c,g)})}else this.sendFn(a,b,void 0,!0)};
r.Zb=function(){var a=this;if(!vr(this))throw Error("IndexedDB is not supported: throttleSend");this.i||(this.i=this.Ca.ma(function(){var b;return A(function(c){if(c.h==1)return c.yield(a.ga.ud("NEW",a.V),2);if(c.h!=3)return b=c.i,b?c.yield(yr(a,b),3):(a.pd(),c.return());a.i&&(a.i=0,a.Zb());c.h=0})},this.Nd))};
r.pd=function(){this.Ca.qa(this.i);this.i=0};
function yr(a,b){var c;return A(function(d){switch(d.h){case 1:if(!vr(a))throw Error("IndexedDB is not supported: immediateSend");if(b.id===void 0){d.A(2);break}return d.yield(a.ga.Je(b.id,a.V),3);case 3:(c=d.i)||a.xb(Error("The request cannot be found in the database."));case 2:if(Ar(a,b,a.Gd)){d.A(4);break}a.xb(Error("Networkless Logging: Stored logs request expired age limit"));if(b.id===void 0){d.A(5);break}return d.yield(a.ga.wb(b.id,a.V),5);case 5:return d.return();case 4:b.skipRetry||(b=Br(a,
b));if(!b){d.A(0);break}if(!b.skipRetry||b.id===void 0){d.A(8);break}return d.yield(a.ga.wb(b.id,a.V),8);case 8:a.sendFn(b.url,b.options,!!b.skipRetry),d.h=0}})}
function Br(a,b){if(!vr(a))throw Error("IndexedDB is not supported: updateRequestHandlers");var c=b.options.onError?b.options.onError:function(){};
b.options.onError=function(e,f){var g,h,k,l;return A(function(m){switch(m.h){case 1:g=Cr(f);(h=Dr(f))&&a.W&&a.W("web_enable_error_204")&&a.handleError(Error("Request failed due to compression"),b.url,f);if(!(a.W&&a.W("nwl_consider_error_code")&&g||a.W&&!a.W("nwl_consider_error_code")&&a.potentialEsfErrorCounter<=a.xc)){m.A(2);break}if(!a.fa.Cc){m.A(3);break}return m.yield(a.fa.Cc(),3);case 3:if(a.fa.ta()){m.A(2);break}c(e,f);if(!a.W||!a.W("nwl_consider_error_code")||((k=b)==null?void 0:k.id)===void 0){m.A(6);
break}return m.yield(a.ga.Yc(b.id,a.V,!1),6);case 6:return m.return();case 2:if(a.W&&a.W("nwl_consider_error_code")&&!g&&a.potentialEsfErrorCounter>a.xc)return m.return();a.potentialEsfErrorCounter++;if(((l=b)==null?void 0:l.id)===void 0){m.A(8);break}return b.sendCount<a.Id?m.yield(a.ga.Yc(b.id,a.V,!0,h?!1:void 0),12):m.yield(a.ga.wb(b.id,a.V),8);case 12:a.Ca.ma(function(){a.fa.ta()&&a.Zb()},a.Hd);
case 8:c(e,f),m.h=0}})};
var d=b.options.onSuccess?b.options.onSuccess:function(){};
b.options.onSuccess=function(e,f){var g;return A(function(h){if(h.h==1)return((g=b)==null?void 0:g.id)===void 0?h.A(2):h.yield(a.ga.wb(b.id,a.V),2);a.fa.kb&&a.W&&a.W("vss_network_hint")&&a.fa.kb(!0);d(e,f);h.h=0})};
return b}
function Ar(a,b,c){b=b.timestamp;return a.now()-b>=c?!1:!0}
function xr(a){if(!vr(a))throw Error("IndexedDB is not supported: retryQueuedRequests");a.ga.ud("QUEUED",a.V).then(function(b){b&&!Ar(a,b,a.Fd)?a.Ca.ma(function(){return A(function(c){if(c.h==1)return b.id===void 0?c.A(2):c.yield(a.ga.Yc(b.id,a.V),2);xr(a);c.h=0})}):a.fa.ta()&&a.Zb()})}
function zr(a,b){a.Ud&&!a.fa.ta()?a.Ud(b):a.handleError(b)}
function vr(a){return!!a.V||a.dc}
function Cr(a){var b;return(a=a==null?void 0:(b=a.error)==null?void 0:b.code)&&a>=400&&a<=599?!1:!0}
function Dr(a){var b;a=a==null?void 0:(b=a.error)==null?void 0:b.code;return!(a!==400&&a!==415)}
;var Er;
function Fr(){if(Er)return Er();var a={};Er=jq("LogsDatabaseV2",{Fb:(a.LogsRequestsStore={Lb:2},a),shared:!1,upgrade:function(b,c,d){c(2)&&pp(b,"LogsRequestsStore",{keyPath:"id",autoIncrement:!0});c(3);c(5)&&(d=d.objectStore("LogsRequestsStore"),d.h.indexNames.contains("newRequest")&&d.h.deleteIndex("newRequest"),wp(d,"newRequestV2",["status","interface","timestamp"]));c(7)&&b.h.objectStoreNames.contains("sapisid")&&b.h.deleteObjectStore("sapisid");c(9)&&b.h.objectStoreNames.contains("SWHealthLog")&&b.h.deleteObjectStore("SWHealthLog")},
version:9});return Er()}
;function Gr(a){return Jp(Fr(),a)}
function Hr(a,b){var c,d,e,f;return A(function(g){if(g.h==1)return c={startTime:U(),infos:{transactionType:"YT_IDB_TRANSACTION_TYPE_WRITE"},ticks:{}},g.yield(Gr(b),2);if(g.h!=3)return d=g.i,e=Object.assign({},a,{options:JSON.parse(JSON.stringify(a.options)),interface:P("INNERTUBE_CONTEXT_CLIENT_NAME",0)}),g.yield(rp(d,"LogsRequestsStore",e),3);f=g.i;c.ticks.tc=U();Ir(c);return g.return(f)})}
function Jr(a,b){var c,d,e,f,g,h,k,l;return A(function(m){if(m.h==1)return c={startTime:U(),infos:{transactionType:"YT_IDB_TRANSACTION_TYPE_READ"},ticks:{}},m.yield(Gr(b),2);if(m.h!=3)return d=m.i,e=P("INNERTUBE_CONTEXT_CLIENT_NAME",0),f=[a,e,0],g=[a,e,U()],h=IDBKeyRange.bound(f,g),k="prev",R("use_fifo_for_networkless")&&(k="next"),l=void 0,m.yield(op(d,["LogsRequestsStore"],{mode:"readwrite",ka:!0},function(n){return Cp(n.objectStore("LogsRequestsStore").index("newRequestV2"),{query:h,direction:k},
function(p){p.getValue()&&(l=p.getValue(),a==="NEW"&&(l.status="QUEUED",p.update(l)))})}),3);
c.ticks.tc=U();Ir(c);return m.return(l)})}
function Kr(a,b){var c;return A(function(d){if(d.h==1)return d.yield(Gr(b),2);c=d.i;return d.return(op(c,["LogsRequestsStore"],{mode:"readwrite",ka:!0},function(e){var f=e.objectStore("LogsRequestsStore");return f.get(a).then(function(g){if(g)return g.status="QUEUED",kp(f.h.put(g,void 0)).then(function(){return g})})}))})}
function Lr(a,b,c,d){c=c===void 0?!0:c;var e;return A(function(f){if(f.h==1)return f.yield(Gr(b),2);e=f.i;return f.return(op(e,["LogsRequestsStore"],{mode:"readwrite",ka:!0},function(g){var h=g.objectStore("LogsRequestsStore");return h.get(a).then(function(k){return k?(k.status="NEW",c&&(k.sendCount+=1),d!==void 0&&(k.options.compress=d),kp(h.h.put(k,void 0)).then(function(){return k})):ep.resolve(void 0)})}))})}
function Mr(a,b){var c;return A(function(d){if(d.h==1)return d.yield(Gr(b),2);c=d.i;return d.return(c.delete("LogsRequestsStore",a))})}
function Nr(a){var b,c;return A(function(d){if(d.h==1)return d.yield(Gr(a),2);b=d.i;c=U()-2592E6;return d.yield(op(b,["LogsRequestsStore"],{mode:"readwrite",ka:!0},function(e){return yp(e.objectStore("LogsRequestsStore"),{},function(f){if(f.getValue().timestamp<=c)return f.delete().then(function(){return zp(f)})})}),0)})}
function Or(){A(function(a){return a.yield(fq(),0)})}
function Ir(a){R("nwl_csi_killswitch")||Sq("networkless_performance",a,{sampleRate:1})}
;var Pr={accountStateChangeSignedIn:23,accountStateChangeSignedOut:24,delayedEventMetricCaptured:11,latencyActionBaselined:6,latencyActionInfo:7,latencyActionTicked:5,offlineTransferStatusChanged:2,offlineImageDownload:335,playbackStartStateChanged:9,systemHealthCaptured:3,mangoOnboardingCompleted:10,mangoPushNotificationReceived:230,mangoUnforkDbMigrationError:121,mangoUnforkDbMigrationSummary:122,mangoUnforkDbMigrationPreunforkDbVersionNumber:133,mangoUnforkDbMigrationPhoneMetadata:134,mangoUnforkDbMigrationPhoneStorage:135,
mangoUnforkDbMigrationStep:142,mangoAsyncApiMigrationEvent:223,mangoDownloadVideoResult:224,mangoHomepageVideoCount:279,mangoHomeV3State:295,mangoImageClientCacheHitEvent:273,sdCardStatusChanged:98,framesDropped:12,thumbnailHovered:13,deviceRetentionInfoCaptured:14,thumbnailLoaded:15,backToAppEvent:318,streamingStatsCaptured:17,offlineVideoShared:19,appCrashed:20,youThere:21,offlineStateSnapshot:22,mdxSessionStarted:25,mdxSessionConnected:26,mdxSessionDisconnected:27,bedrockResourceConsumptionSnapshot:28,
nextGenWatchWatchSwiped:29,kidsAccountsSnapshot:30,zeroStepChannelCreated:31,tvhtml5SearchCompleted:32,offlineSharePairing:34,offlineShareUnlock:35,mdxRouteDistributionSnapshot:36,bedrockRepetitiveActionTimed:37,unpluggedDegradationInfo:229,uploadMp4HeaderMoved:38,uploadVideoTranscoded:39,uploadProcessorStarted:46,uploadProcessorEnded:47,uploadProcessorReady:94,uploadProcessorRequirementPending:95,uploadProcessorInterrupted:96,uploadFrontendEvent:241,assetPackDownloadStarted:41,assetPackDownloaded:42,
assetPackApplied:43,assetPackDeleted:44,appInstallAttributionEvent:459,playbackSessionStopped:45,adBlockerMessagingShown:48,distributionChannelCaptured:49,dataPlanCpidRequested:51,detailedNetworkTypeCaptured:52,sendStateUpdated:53,receiveStateUpdated:54,sendDebugStateUpdated:55,receiveDebugStateUpdated:56,kidsErrored:57,mdxMsnSessionStatsFinished:58,appSettingsCaptured:59,mdxWebSocketServerHttpError:60,mdxWebSocketServer:61,startupCrashesDetected:62,coldStartInfo:435,offlinePlaybackStarted:63,liveChatMessageSent:225,
liveChatUserPresent:434,liveChatBeingModerated:457,liveCreationCameraUpdated:64,liveCreationEncodingCaptured:65,liveCreationError:66,liveCreationHealthUpdated:67,liveCreationVideoEffectsCaptured:68,liveCreationStageOccured:75,liveCreationBroadcastScheduled:123,liveCreationArchiveReplacement:149,liveCreationCostreamingConnection:421,liveCreationStreamWebrtcStats:288,mdxSessionRecoveryStarted:69,mdxSessionRecoveryCompleted:70,mdxSessionRecoveryStopped:71,visualElementShown:72,visualElementHidden:73,
visualElementGestured:78,visualElementStateChanged:208,screenCreated:156,playbackAssociated:202,visualElementAttached:215,playbackContextEvent:214,cloudCastingPlaybackStarted:74,webPlayerApiCalled:76,tvhtml5AccountDialogOpened:79,foregroundHeartbeat:80,foregroundHeartbeatScreenAssociated:111,kidsOfflineSnapshot:81,mdxEncryptionSessionStatsFinished:82,playerRequestCompleted:83,liteSchedulerStatistics:84,mdxSignIn:85,spacecastMetadataLookupRequested:86,spacecastBatchLookupRequested:87,spacecastSummaryRequested:88,
spacecastPlayback:89,spacecastDiscovery:90,tvhtml5LaunchUrlComponentChanged:91,mdxBackgroundPlaybackRequestCompleted:92,mdxBrokenAdditionalDataDeviceDetected:93,tvhtml5LocalStorage:97,tvhtml5DeviceStorageStatus:147,autoCaptionsAvailable:99,playbackScrubbingEvent:339,flexyState:100,interfaceOrientationCaptured:101,mainAppBrowseFragmentCache:102,offlineCacheVerificationFailure:103,offlinePlaybackExceptionDigest:217,vrCopresenceStats:104,vrCopresenceSyncStats:130,vrCopresenceCommsStats:137,vrCopresencePartyStats:153,
vrCopresenceEmojiStats:213,vrCopresenceEvent:141,vrCopresenceFlowTransitEvent:160,vrCowatchPartyEvent:492,vrCowatchUserStartOrJoinEvent:504,vrPlaybackEvent:345,kidsAgeGateTracking:105,offlineDelayAllowedTracking:106,mainAppAutoOfflineState:107,videoAsThumbnailDownload:108,videoAsThumbnailPlayback:109,liteShowMore:110,renderingError:118,kidsProfilePinGateTracking:119,abrTrajectory:124,scrollEvent:125,streamzIncremented:126,kidsProfileSwitcherTracking:127,kidsProfileCreationTracking:129,buyFlowStarted:136,
mbsConnectionInitiated:138,mbsPlaybackInitiated:139,mbsLoadChildren:140,liteProfileFetcher:144,mdxRemoteTransaction:146,reelPlaybackError:148,reachabilityDetectionEvent:150,mobilePlaybackEvent:151,courtsidePlayerStateChanged:152,musicPersistentCacheChecked:154,musicPersistentCacheCleared:155,playbackInterrupted:157,playbackInterruptionResolved:158,fixFopFlow:159,anrDetection:161,backstagePostCreationFlowEnded:162,clientError:163,gamingAccountLinkStatusChanged:164,liteHousewarming:165,buyFlowEvent:167,
kidsParentalGateTracking:168,kidsSignedOutSettingsStatus:437,kidsSignedOutPauseHistoryFixStatus:438,tvhtml5WatchdogViolation:444,ypcUpgradeFlow:169,yongleStudy:170,ypcUpdateFlowStarted:171,ypcUpdateFlowCancelled:172,ypcUpdateFlowSucceeded:173,ypcUpdateFlowFailed:174,liteGrowthkitPromo:175,paymentFlowStarted:341,transactionFlowShowPaymentDialog:405,transactionFlowStarted:176,transactionFlowSecondaryDeviceStarted:222,transactionFlowSecondaryDeviceSignedOutStarted:383,transactionFlowCancelled:177,transactionFlowPaymentCallBackReceived:387,
transactionFlowPaymentSubmitted:460,transactionFlowPaymentSucceeded:329,transactionFlowSucceeded:178,transactionFlowFailed:179,transactionFlowPlayBillingConnectionStartEvent:428,transactionFlowSecondaryDeviceSuccess:458,transactionFlowErrorEvent:411,liteVideoQualityChanged:180,watchBreakEnablementSettingEvent:181,watchBreakFrequencySettingEvent:182,videoEffectsCameraPerformanceMetrics:183,adNotify:184,startupTelemetry:185,playbackOfflineFallbackUsed:186,outOfMemory:187,ypcPauseFlowStarted:188,ypcPauseFlowCancelled:189,
ypcPauseFlowSucceeded:190,ypcPauseFlowFailed:191,uploadFileSelected:192,ypcResumeFlowStarted:193,ypcResumeFlowCancelled:194,ypcResumeFlowSucceeded:195,ypcResumeFlowFailed:196,adsClientStateChange:197,ypcCancelFlowStarted:198,ypcCancelFlowCancelled:199,ypcCancelFlowSucceeded:200,ypcCancelFlowFailed:201,ypcCancelFlowGoToPaymentProcessor:402,ypcDeactivateFlowStarted:320,ypcRedeemFlowStarted:203,ypcRedeemFlowCancelled:204,ypcRedeemFlowSucceeded:205,ypcRedeemFlowFailed:206,ypcFamilyCreateFlowStarted:258,
ypcFamilyCreateFlowCancelled:259,ypcFamilyCreateFlowSucceeded:260,ypcFamilyCreateFlowFailed:261,ypcFamilyManageFlowStarted:262,ypcFamilyManageFlowCancelled:263,ypcFamilyManageFlowSucceeded:264,ypcFamilyManageFlowFailed:265,restoreContextEvent:207,embedsAdEvent:327,autoplayTriggered:209,clientDataErrorEvent:210,experimentalVssValidation:211,tvhtml5TriggeredEvent:212,tvhtml5FrameworksFieldTrialResult:216,tvhtml5FrameworksFieldTrialStart:220,musicOfflinePreferences:218,watchTimeSegment:219,appWidthLayoutError:221,
accountRegistryChange:226,userMentionAutoCompleteBoxEvent:227,downloadRecommendationEnablementSettingEvent:228,musicPlaybackContentModeChangeEvent:231,offlineDbOpenCompleted:232,kidsFlowEvent:233,kidsFlowCorpusSelectedEvent:234,videoEffectsEvent:235,unpluggedOpsEogAnalyticsEvent:236,playbackAudioRouteEvent:237,interactionLoggingDebugModeError:238,offlineYtbRefreshed:239,kidsFlowError:240,musicAutoplayOnLaunchAttempted:242,deviceContextActivityEvent:243,deviceContextEvent:244,templateResolutionException:245,
musicSideloadedPlaylistServiceCalled:246,embedsStorageAccessNotChecked:247,embedsHasStorageAccessResult:248,embedsItpPlayedOnReload:249,embedsRequestStorageAccessResult:250,embedsShouldRequestStorageAccessResult:251,embedsRequestStorageAccessState:256,embedsRequestStorageAccessFailedState:257,embedsItpWatchLaterResult:266,searchSuggestDecodingPayloadFailure:252,siriShortcutActivated:253,tvhtml5KeyboardPerformance:254,latencyActionSpan:255,elementsLog:267,ytbFileOpened:268,tfliteModelError:269,apiTest:270,
yongleUsbSetup:271,touStrikeInterstitialEvent:272,liteStreamToSave:274,appBundleClientEvent:275,ytbFileCreationFailed:276,adNotifyFailure:278,ytbTransferFailed:280,blockingRequestFailed:281,liteAccountSelector:282,liteAccountUiCallbacks:283,dummyPayload:284,browseResponseValidationEvent:285,entitiesError:286,musicIosBackgroundFetch:287,mdxNotificationEvent:289,layersValidationError:290,musicPwaInstalled:291,liteAccountCleanup:292,html5PlayerHealthEvent:293,watchRestoreAttempt:294,liteAccountSignIn:296,
notaireEvent:298,kidsVoiceSearchEvent:299,adNotifyFilled:300,delayedEventDropped:301,analyticsSearchEvent:302,systemDarkThemeOptOutEvent:303,flowEvent:304,networkConnectivityBaselineEvent:305,ytbFileImported:306,downloadStreamUrlExpired:307,directSignInEvent:308,lyricImpressionEvent:309,accessibilityStateEvent:310,tokenRefreshEvent:311,genericAttestationExecution:312,tvhtml5VideoSeek:313,unpluggedAutoPause:314,scrubbingEvent:315,bedtimeReminderEvent:317,tvhtml5UnexpectedRestart:319,tvhtml5StabilityTraceEvent:478,
tvhtml5OperationHealth:467,tvhtml5WatchKeyEvent:321,voiceLanguageChanged:322,tvhtml5LiveChatStatus:323,parentToolsCorpusSelectedEvent:324,offerAdsEnrollmentInitiated:325,networkQualityIntervalEvent:326,deviceStartupMetrics:328,heartbeatActionPlayerTransitioned:330,tvhtml5Lifecycle:331,heartbeatActionPlayerHalted:332,adaptiveInlineMutedSettingEvent:333,mainAppLibraryLoadingState:334,thirdPartyLogMonitoringEvent:336,appShellAssetLoadReport:337,tvhtml5AndroidAttestation:338,tvhtml5StartupSoundEvent:340,
iosBackgroundRefreshTask:342,iosBackgroundProcessingTask:343,sliEventBatch:344,postImpressionEvent:346,musicSideloadedPlaylistExport:347,idbUnexpectedlyClosed:348,voiceSearchEvent:349,mdxSessionCastEvent:350,idbQuotaExceeded:351,idbTransactionEnded:352,idbTransactionAborted:353,tvhtml5KeyboardLogging:354,idbIsSupportedCompleted:355,creatorStudioMobileEvent:356,idbDataCorrupted:357,parentToolsAppChosenEvent:358,webViewBottomSheetResized:359,activeStateControllerScrollPerformanceSummary:360,navigatorValidation:361,
mdxSessionHeartbeat:362,clientHintsPolyfillDiagnostics:363,clientHintsPolyfillEvent:364,proofOfOriginTokenError:365,kidsAddedAccountSummary:366,musicWearableDevice:367,ypcRefundFlowEvent:368,tvhtml5PlaybackMeasurementEvent:369,tvhtml5WatermarkMeasurementEvent:370,clientExpGcfPropagationEvent:371,mainAppReferrerIntent:372,leaderLockEnded:373,leaderLockAcquired:374,googleHatsEvent:375,persistentLensLaunchEvent:376,parentToolsChildWelcomeChosenEvent:378,browseThumbnailPreloadEvent:379,finalPayload:380,
mdxDialAdditionalDataUpdateEvent:381,webOrchestrationTaskLifecycleRecord:382,startupSignalEvent:384,accountError:385,gmsDeviceCheckEvent:386,accountSelectorEvent:388,accountUiCallbacks:389,mdxDialAdditionalDataProbeEvent:390,downloadsSearchIcingApiStats:391,downloadsSearchIndexUpdatedEvent:397,downloadsSearchIndexSnapshot:398,dataPushClientEvent:392,kidsCategorySelectedEvent:393,mdxDeviceManagementSnapshotEvent:394,prefetchRequested:395,prefetchableCommandExecuted:396,gelDebuggingEvent:399,webLinkTtsPlayEnd:400,
clipViewInvalid:401,persistentStorageStateChecked:403,cacheWipeoutEvent:404,playerEvent:410,sfvEffectPipelineStartedEvent:412,sfvEffectPipelinePausedEvent:429,sfvEffectPipelineEndedEvent:413,sfvEffectChosenEvent:414,sfvEffectLoadedEvent:415,sfvEffectUserInteractionEvent:465,sfvEffectFirstFrameProcessedLatencyEvent:416,sfvEffectAggregatedFramesProcessedLatencyEvent:417,sfvEffectAggregatedFramesDroppedEvent:418,sfvEffectPipelineErrorEvent:430,sfvEffectGraphFrozenEvent:419,sfvEffectGlThreadBlockedEvent:420,
mdeQosEvent:510,mdeVideoChangedEvent:442,mdePlayerPerformanceMetrics:472,mdeExporterEvent:497,genericClientExperimentEvent:423,homePreloadTaskScheduled:424,homePreloadTaskExecuted:425,homePreloadCacheHit:426,polymerPropertyChangedInObserver:427,applicationStarted:431,networkCronetRttBatch:432,networkCronetRttSummary:433,repeatChapterLoopEvent:436,seekCancellationEvent:462,lockModeTimeoutEvent:483,externalVideoShareToYoutubeAttempt:501,parentCodeEvent:502,offlineTransferStarted:4,musicOfflineMixtapePreferencesChanged:16,
mangoDailyNewVideosNotificationAttempt:40,mangoDailyNewVideosNotificationError:77,dtwsPlaybackStarted:112,dtwsTileFetchStarted:113,dtwsTileFetchCompleted:114,dtwsTileFetchStatusChanged:145,dtwsKeyframeDecoderBufferSent:115,dtwsTileUnderflowedOnNonkeyframe:116,dtwsBackfillFetchStatusChanged:143,dtwsBackfillUnderflowed:117,dtwsAdaptiveLevelChanged:128,blockingVisitorIdTimeout:277,liteSocial:18,mobileJsInvocation:297,biscottiBasedDetection:439,coWatchStateChange:440,embedsVideoDataDidChange:441,shortsFirst:443,
cruiseControlEvent:445,qoeClientLoggingContext:446,atvRecommendationJobExecuted:447,tvhtml5UserFeedback:448,producerProjectCreated:449,producerProjectOpened:450,producerProjectDeleted:451,producerProjectElementAdded:453,producerProjectElementRemoved:454,producerAppStateChange:509,tvhtml5ShowClockEvent:455,deviceCapabilityCheckMetrics:456,youtubeClearcutEvent:461,offlineBrowseFallbackEvent:463,getCtvTokenEvent:464,startupDroppedFramesSummary:466,screenshotEvent:468,miniAppPlayEvent:469,elementsDebugCounters:470,
fontLoadEvent:471,webKillswitchReceived:473,webKillswitchExecuted:474,cameraOpenEvent:475,manualSmoothnessMeasurement:476,tvhtml5AppQualityEvent:477,polymerPropertyAccessEvent:479,miniAppSdkUsage:480,cobaltTelemetryEvent:481,crossDevicePlayback:482,channelCreatedWithObakeImage:484,channelEditedWithObakeImage:485,offlineDeleteEvent:486,crossDeviceNotificationTransfer:487,androidIntentEvent:488,unpluggedAmbientInterludesCounterfactualEvent:489,keyPlaysPlayback:490,shortsCreationFallbackEvent:493,vssData:491,
castMatch:494,miniAppPerformanceMetrics:495,userFeedbackEvent:496,kidsGuestSessionMismatch:498,musicSideloadedPlaylistMigrationEvent:499,sleepTimerSessionFinishEvent:500,watchEpPromoConflict:503,innertubeResponseCacheMetrics:505,miniAppAdEvent:506,dataPlanUpsellEvent:507,producerProjectRenamed:508,producerMediaSelectionEvent:511,embedsAutoplayStatusChanged:512,remoteConnectEvent:513,connectedSessionMisattributionEvent:514,producerProjectElementModified:515};var Qr={},Rr=jq("ServiceWorkerLogsDatabase",{Fb:(Qr.SWHealthLog={Lb:1},Qr),shared:!0,upgrade:function(a,b){b(1)&&wp(pp(a,"SWHealthLog",{keyPath:"id",autoIncrement:!0}),"swHealthNewRequest",["interface","timestamp"])},
version:1});function Sr(a){return Jp(Rr(),a)}
function Tr(a){var b,c;A(function(d){if(d.h==1)return d.yield(Sr(a),2);b=d.i;c=U()-2592E6;return d.yield(op(b,["SWHealthLog"],{mode:"readwrite",ka:!0},function(e){return yp(e.objectStore("SWHealthLog"),{},function(f){if(f.getValue().timestamp<=c)return f.delete().then(function(){return zp(f)})})}),0)})}
function Ur(a){var b;return A(function(c){if(c.h==1)return c.yield(Sr(a),2);b=c.i;return c.yield(b.clear("SWHealthLog"),0)})}
;var Vr={},Wr=0;function Xr(a){var b=b===void 0?{}:b;var c=new Image,d=""+Wr++;Vr[d]=c;c.onload=c.onerror=function(){delete Vr[d]};
b.Sh&&(c.referrerPolicy="no-referrer");c.src=a}
;var Yr;function Zr(){Yr||(Yr=new Ao("yt.offline"));return Yr}
function $r(a){if(R("offline_error_handling")){var b=Zr().get("errors",!0)||{};b[a.message]={name:a.name,stack:a.stack};a.level&&(b[a.message].level=a.level);Zr().set("errors",b,2592E3,!0)}}
;function as(){this.h=new Map;this.i=!1}
function bs(){if(!as.h){var a=E("yt.networkRequestMonitor.instance")||new as;D("yt.networkRequestMonitor.instance",a);as.h=a}return as.h}
as.prototype.requestComplete=function(a,b){b&&(this.i=!0);a=this.removeParams(a);this.h.get(a)||this.h.set(a,b)};
as.prototype.isEndpointCFR=function(a){a=this.removeParams(a);return(a=this.h.get(a))?!1:a===!1&&this.i?!0:null};
as.prototype.removeParams=function(a){return a.split("?")[0]};
as.prototype.removeParams=as.prototype.removeParams;as.prototype.isEndpointCFR=as.prototype.isEndpointCFR;as.prototype.requestComplete=as.prototype.requestComplete;as.getInstance=bs;function cs(){di.call(this);var a=this;this.j=!1;this.h=Zj();this.h.listen("networkstatus-online",function(){if(a.j&&R("offline_error_handling")){var b=Zr().get("errors",!0);if(b){for(var c in b)if(b[c]){var d=new T(c,"sent via offline_errors");d.name=b[c].name;d.stack=b[c].stack;d.level=b[c].level;wm(d)}Zr().set("errors",{},2592E3,!0)}}})}
w(cs,di);function ds(){if(!cs.h){var a=E("yt.networkStatusManager.instance")||new cs;D("yt.networkStatusManager.instance",a);cs.h=a}return cs.h}
r=cs.prototype;r.ta=function(){return this.h.ta()};
r.kb=function(a){this.h.h=a};
r.ye=function(){var a=window.navigator.onLine;return a===void 0?!0:a};
r.oe=function(){this.j=!0};
r.listen=function(a,b){return this.h.listen(a,b)};
r.Cc=function(a){a=Xj(this.h,a);a.then(function(b){R("use_cfr_monitor")&&bs().requestComplete("generate_204",b)});
return a};
cs.prototype.sendNetworkCheckRequest=cs.prototype.Cc;cs.prototype.listen=cs.prototype.listen;cs.prototype.enableErrorFlushing=cs.prototype.oe;cs.prototype.getWindowStatus=cs.prototype.ye;cs.prototype.networkStatusHint=cs.prototype.kb;cs.prototype.isNetworkAvailable=cs.prototype.ta;cs.getInstance=ds;function es(a){a=a===void 0?{}:a;di.call(this);var b=this;this.h=this.u=0;this.j=ds();var c=E("yt.networkStatusManager.instance.listen").bind(this.j);c&&(a.rateLimit?(this.rateLimit=a.rateLimit,c("networkstatus-online",function(){gs(b,"publicytnetworkstatus-online")}),c("networkstatus-offline",function(){gs(b,"publicytnetworkstatus-offline")})):(c("networkstatus-online",function(){ei(b,"publicytnetworkstatus-online")}),c("networkstatus-offline",function(){ei(b,"publicytnetworkstatus-offline")})))}
w(es,di);es.prototype.ta=function(){var a=E("yt.networkStatusManager.instance.isNetworkAvailable");return a?a.bind(this.j)():!0};
es.prototype.kb=function(a){var b=E("yt.networkStatusManager.instance.networkStatusHint").bind(this.j);b&&b(a)};
es.prototype.Cc=function(a){var b=this,c;return A(function(d){c=E("yt.networkStatusManager.instance.sendNetworkCheckRequest").bind(b.j);return R("skip_network_check_if_cfr")&&bs().isEndpointCFR("generate_204")?d.return(new Promise(function(e){var f;b.kb(((f=window.navigator)==null?void 0:f.onLine)||!0);e(b.ta())})):c?d.return(c(a)):d.return(!0)})};
function gs(a,b){a.rateLimit?a.h?(ak.qa(a.u),a.u=ak.ma(function(){a.o!==b&&(ei(a,b),a.o=b,a.h=U())},a.rateLimit-(U()-a.h))):(ei(a,b),a.o=b,a.h=U()):ei(a,b)}
;var hs;function is(){var a=ur.call;hs||(hs=new es({Jh:!0,Ah:!0}));a.call(ur,this,{ga:{ee:Nr,wb:Mr,ud:Jr,Je:Kr,Yc:Lr,set:Hr},fa:hs,handleError:function(b,c,d){var e,f=d==null?void 0:(e=d.error)==null?void 0:e.code;if(f===400||f===415){var g;xm(new T(b.message,c,d==null?void 0:(g=d.error)==null?void 0:g.code),void 0,void 0,void 0,!0)}else wm(b)},
xb:xm,sendFn:js,now:U,Ud:$r,Ca:zo(),Xc:"publicytnetworkstatus-online",Uc:"publicytnetworkstatus-offline",jc:!0,hc:.1,xc:S("potential_esf_error_limit",10),W:R,Pb:!(Rn()&&ks())});this.j=new yj;R("networkless_immediately_drop_all_requests")&&Or();gq("LogsDatabaseV2")}
w(is,ur);function ls(){var a=E("yt.networklessRequestController.instance");a||(a=new is,D("yt.networklessRequestController.instance",a),R("networkless_logging")&&Wp().then(function(b){a.V=b;wr(a);a.j.resolve();a.jc&&Math.random()<=a.hc&&a.V&&Tr(a.V);R("networkless_immediately_drop_sw_health_store")&&ms(a)}));
return a}
is.prototype.writeThenSend=function(a,b){b||(b={});b=ns(a,b);Rn()||(this.h=!1);ur.prototype.writeThenSend.call(this,a,b)};
is.prototype.sendThenWrite=function(a,b,c){b||(b={});b=ns(a,b);Rn()||(this.h=!1);ur.prototype.sendThenWrite.call(this,a,b,c)};
is.prototype.sendAndWrite=function(a,b){b||(b={});b=ns(a,b);Rn()||(this.h=!1);ur.prototype.sendAndWrite.call(this,a,b)};
is.prototype.awaitInitialization=function(){return this.j.promise};
function ms(a){var b;A(function(c){if(!a.V)throw b=ap("clearSWHealthLogsDb"),b;return c.return(Ur(a.V).catch(function(d){a.handleError(d)}))})}
function js(a,b,c,d){d=d===void 0?!1:d;b=R("web_fp_via_jspb")?Object.assign({},b):b;R("use_cfr_monitor")&&ps(a,b);if(R("use_request_time_ms_header"))b.headers&&Im(a)&&(b.headers["X-Goog-Request-Time"]=JSON.stringify(Math.round(U())));else{var e;if((e=b.postParams)==null?0:e.requestTimeMs)b.postParams.requestTimeMs=Math.round(U())}if(c&&Object.keys(b).length===0){var f=f===void 0?"":f;var g=g===void 0?!1:g;var h=h===void 0?!1:h;if(a)if(f)Vm(a,void 0,"POST",f,void 0);else if(P("USE_NET_AJAX_FOR_PING_TRANSPORT",
!1)||h)Vm(a,void 0,"GET","",void 0,void 0,g,h);else{b:{try{var k=new fb({url:a});if(k.u?typeof k.j!=="string"||k.j.length===0?0:{version:3,le:k.j,be:gb(k,"&act=1&ri=1"+hb(k))}:k.M&&{version:4,le:gb(k,"&dct=1&suid="+k.o),be:gb(k,"&act=1&ri=1&suid="+k.o)}){var l=fc(hc(5,a)),m;if(!(m=!l||!l.endsWith("/aclk"))){var n=a.search(pc),p=oc(a,0,"ri",n);if(p<0)var t=null;else{var u=a.indexOf("&",p);if(u<0||u>n)u=n;t=decodeURIComponent(a.slice(p+3,u!==-1?u:0).replace(/\+/g," "))}m=t!=="1"}var x=!m;break b}}catch(H){}x=
!1}if(x){b:{try{if(window.navigator&&window.navigator.sendBeacon&&window.navigator.sendBeacon(a,"")){var z=!0;break b}}catch(H){}z=!1}c=z?!0:!1}else c=!1;c||Xr(a)}}else b.compress?b.postBody?(typeof b.postBody!=="string"&&(b.postBody=JSON.stringify(b.postBody)),jr(a,b.postBody,b,Zm,d)):jr(a,JSON.stringify(b.postParams),b,Ym,d):Zm(a,b)}
function ns(a,b){R("use_event_time_ms_header")&&Im(a)&&(b.headers||(b.headers={}),b.headers["X-Goog-Event-Time"]=JSON.stringify(Math.round(U())));return b}
function ps(a,b){var c=b.onError?b.onError:function(){};
b.onError=function(e,f){bs().requestComplete(a,!1);c(e,f)};
var d=b.onSuccess?b.onSuccess:function(){};
b.onSuccess=function(e,f){bs().requestComplete(a,!0);d(e,f)}}
function ks(){return ic(document.location.toString())!=="www.youtube-nocookie.com"}
;var qs=!1,rs=C.ytNetworklessLoggingInitializationOptions||{isNwlInitialized:qs};D("ytNetworklessLoggingInitializationOptions",rs);function ss(){var a;A(function(b){if(b.h==1)return b.yield(Wp(),2);a=b.i;if(!a||!Rn()&&!R("nwl_init_require_datasync_id_killswitch")||!ks())return b.A(0);qs=!0;rs.isNwlInitialized=qs;return b.yield(ls().awaitInitialization(),0)})}
;function ts(a){var b=this;this.config_=null;a?this.config_=a:xq()&&(this.config_=yq());Un(function(){sr(b)},5E3)}
ts.prototype.isReady=function(){!this.config_&&xq()&&(this.config_=yq());return!!this.config_};
function tr(a,b,c,d){function e(n){n=n===void 0?!1:n;var p;if(d.retry&&h!="www.youtube-nocookie.com"&&(n||R("skip_ls_gel_retry")||g.headers["Content-Type"]!=="application/json"||(p=qr(b,c,l,k)),p)){var t=g.onSuccess,u=g.onFetchSuccess;g.onSuccess=function(H,K){rr(p);t(H,K)};
c.onFetchSuccess=function(H,K){rr(p);u(H,K)}}try{if(n&&d.retry&&!d.networklessOptions.bypassNetworkless)g.method="POST",d.networklessOptions.writeThenSend?ls().writeThenSend(m,g):ls().sendAndWrite(m,g);
else if(d.compress){var x=!d.networklessOptions.writeThenSend;if(g.postBody){var z=g.postBody;typeof z!=="string"&&(z=JSON.stringify(g.postBody));jr(m,z,g,Zm,x)}else jr(m,JSON.stringify(g.postParams),g,Ym,x)}else R("web_all_payloads_via_jspb")?Zm(m,g):Ym(m,g)}catch(H){if(H.name==="InvalidAccessError")p&&(rr(p),p=0),xm(Error("An extension is blocking network request."));else throw H;}p&&Un(function(){sr(a)},5E3)}
!P("VISITOR_DATA")&&b!=="visitor_id"&&Math.random()<.01&&xm(new T("Missing VISITOR_DATA when sending innertube request.",b,c,d));if(!a.isReady()){var f=new T("innertube xhrclient not ready",b,c,d);wm(f);throw f;}var g={headers:d.headers||{},method:"POST",postParams:c,postBody:d.postBody,postBodyFormat:d.postBodyFormat||"JSON",onTimeout:function(){d.onTimeout()},
onFetchTimeout:d.onTimeout,onSuccess:function(n,p){if(d.onSuccess)d.onSuccess(p)},
onFetchSuccess:function(n){if(d.onSuccess)d.onSuccess(n)},
onError:function(n,p){if(d.onError)d.onError(p)},
onFetchError:function(n){if(d.onError)d.onError(n)},
timeout:d.timeout,withCredentials:!0,compress:d.compress};g.headers["Content-Type"]||(g.headers["Content-Type"]="application/json");var h="";(f=a.config_.Ee)&&(h=f);var k=a.config_.Fe||!1,l=Aq(k,h,d);Object.assign(g.headers,l);g.headers.Authorization&&!h&&k&&(g.headers["x-origin"]=window.location.origin);var m=Gm(""+h+("/youtubei/"+a.config_.innertubeApiVersion+"/"+b),{alt:"json"});(E("ytNetworklessLoggingInitializationOptions")?rs.isNwlInitialized:qs)?Up().then(function(n){e(n)}):e(!1)}
;var us=0,vs=fd?"webkit":ed?"moz":cd?"ms":bd?"o":"";D("ytDomDomGetNextId",E("ytDomDomGetNextId")||function(){return++us});var ws={stopImmediatePropagation:1,stopPropagation:1,preventMouseEvent:1,preventManipulation:1,preventDefault:1,layerX:1,layerY:1,screenX:1,screenY:1,scale:1,rotation:1,webkitMovementX:1,webkitMovementY:1};
function Bs(a){this.type="";this.state=this.source=this.data=this.currentTarget=this.relatedTarget=this.target=null;this.charCode=this.keyCode=0;this.metaKey=this.shiftKey=this.ctrlKey=this.altKey=!1;this.rotation=this.clientY=this.clientX=0;this.scale=1;this.changedTouches=this.touches=null;try{if(a=a||window.event){this.event=a;for(var b in a)b in ws||(this[b]=a[b]);this.scale=a.scale;this.rotation=a.rotation;var c=a.target||a.srcElement;c&&c.nodeType==3&&(c=c.parentNode);this.target=c;var d=a.relatedTarget;
if(d)try{d=d.nodeName?d:null}catch(e){d=null}else this.type=="mouseover"?d=a.fromElement:this.type=="mouseout"&&(d=a.toElement);this.relatedTarget=d;this.clientX=a.clientX!=void 0?a.clientX:a.pageX;this.clientY=a.clientY!=void 0?a.clientY:a.pageY;this.keyCode=a.keyCode?a.keyCode:a.which;this.charCode=a.charCode||(this.type=="keypress"?this.keyCode:0);this.altKey=a.altKey;this.ctrlKey=a.ctrlKey;this.shiftKey=a.shiftKey;this.metaKey=a.metaKey;this.h=a.pageX;this.i=a.pageY}}catch(e){}}
function Cs(a){if(document.body&&document.documentElement){var b=document.body.scrollTop+document.documentElement.scrollTop;a.h=a.clientX+(document.body.scrollLeft+document.documentElement.scrollLeft);a.i=a.clientY+b}}
Bs.prototype.preventDefault=function(){this.event&&(this.event.returnValue=!1,this.event.preventDefault&&this.event.preventDefault())};
Bs.prototype.stopPropagation=function(){this.event&&(this.event.cancelBubble=!0,this.event.stopPropagation&&this.event.stopPropagation())};
Bs.prototype.stopImmediatePropagation=function(){this.event&&(this.event.cancelBubble=!0,this.event.stopImmediatePropagation&&this.event.stopImmediatePropagation())};var vg=C.ytEventsEventsListeners||{};D("ytEventsEventsListeners",vg);var Ds=C.ytEventsEventsCounter||{count:0};D("ytEventsEventsCounter",Ds);
function Es(a,b,c,d){d=d===void 0?{}:d;a.addEventListener&&(b!="mouseenter"||"onmouseenter"in document?b!="mouseleave"||"onmouseenter"in document?b=="mousewheel"&&"MozBoxSizing"in document.documentElement.style&&(b="MozMousePixelScroll"):b="mouseout":b="mouseover");return ug(function(e){var f=typeof e[4]==="boolean"&&e[4]==!!d,g=Sa(e[4])&&Sa(d)&&zg(e[4],d);return!!e.length&&e[0]==a&&e[1]==b&&e[2]==c&&(f||g)})}
function Fs(a,b,c,d){d=d===void 0?{}:d;if(!a||!a.addEventListener&&!a.attachEvent)return"";var e=Es(a,b,c,d);if(e)return e;e=++Ds.count+"";var f=!(b!="mouseenter"&&b!="mouseleave"||!a.addEventListener||"onmouseenter"in document);var g=f?function(h){h=new Bs(h);if(!Jg(h.relatedTarget,function(k){return k==a}))return h.currentTarget=a,h.type=b,c.call(a,h)}:function(h){h=new Bs(h);
h.currentTarget=a;return c.call(a,h)};
g=um(g);a.addEventListener?(b=="mouseenter"&&f?b="mouseover":b=="mouseleave"&&f?b="mouseout":b=="mousewheel"&&"MozBoxSizing"in document.documentElement.style&&(b="MozMousePixelScroll"),Gs()||typeof d==="boolean"?a.addEventListener(b,g,d):a.addEventListener(b,g,!!d.capture)):a.attachEvent("on"+b,g);vg[e]=[a,b,c,g,d];return e}
function Hs(a){a&&(typeof a=="string"&&(a=[a]),Sb(a,function(b){if(b in vg){var c=vg[b],d=c[0],e=c[1],f=c[3];c=c[4];d.removeEventListener?Gs()||typeof c==="boolean"?d.removeEventListener(e,f,c):d.removeEventListener(e,f,!!c.capture):d.detachEvent&&d.detachEvent("on"+e,f);delete vg[b]}}))}
var Gs=ti(function(){var a=!1;try{var b=Object.defineProperty({},"capture",{get:function(){a=!0}});
window.addEventListener("test",null,b)}catch(c){}return a});function Is(a){this.G=a;this.h=null;this.o=0;this.D=null;this.u=0;this.i=[];for(a=0;a<4;a++)this.i.push(0);this.j=0;this.U=Fs(window,"mousemove",Za(this.Y,this));a=Za(this.P,this);typeof a==="function"&&(a=um(a));this.Z=window.setInterval(a,25)}
cb(Is,F);Is.prototype.Y=function(a){a.h===void 0&&Cs(a);var b=a.h;a.i===void 0&&Cs(a);this.h=new rg(b,a.i)};
Is.prototype.P=function(){if(this.h){var a=U();if(this.o!=0){var b=this.D,c=this.h,d=b.x-c.x;b=b.y-c.y;d=Math.sqrt(d*d+b*b)/(a-this.o);this.i[this.j]=Math.abs((d-this.u)/this.u)>.5?1:0;for(c=b=0;c<4;c++)b+=this.i[c]||0;b>=3&&this.G();this.u=d}this.o=a;this.D=this.h;this.j=(this.j+1)%4}};
Is.prototype.ba=function(){window.clearInterval(this.Z);Hs(this.U)};var Js={};
function Ks(a){var b=a===void 0?{}:a;a=b.Ue===void 0?!1:b.Ue;b=b.pe===void 0?!0:b.pe;if(E("_lact",window)==null){var c=parseInt(P("LACT"),10);c=isFinite(c)?Date.now()-Math.max(c,0):-1;D("_lact",c,window);D("_fact",c,window);c==-1&&Ls();Fs(document,"keydown",Ls);Fs(document,"keyup",Ls);Fs(document,"mousedown",Ls);Fs(document,"mouseup",Ls);a?Fs(window,"touchmove",function(){Ms("touchmove",200)},{passive:!0}):(Fs(window,"resize",function(){Ms("resize",200)}),b&&Fs(window,"scroll",function(){Ms("scroll",200)}));
new Is(function(){Ms("mouse",100)});
Fs(document,"touchstart",Ls,{passive:!0});Fs(document,"touchend",Ls,{passive:!0})}}
function Ms(a,b){Js[a]||(Js[a]=!0,ak.ma(function(){Ls();Js[a]=!1},b))}
function Ls(){E("_lact",window)==null&&Ks();var a=Date.now();D("_lact",a,window);E("_fact",window)==-1&&D("_fact",a,window);(a=E("ytglobal.ytUtilActivityCallback_"))&&a()}
function Ns(){var a=E("_lact",window);return a==null?-1:Math.max(Date.now()-a,0)}
;var Os=C.ytPubsubPubsubInstance||new M,Ps=C.ytPubsubPubsubSubscribedKeys||{},Qs=C.ytPubsubPubsubTopicToKeys||{},Rs=C.ytPubsubPubsubIsSynchronous||{};function Ss(a,b){var c=Ts();if(c&&b){var d=c.subscribe(a,function(){function e(){Ps[d]&&b.apply&&typeof b.apply=="function"&&b.apply(window,f)}
var f=arguments;try{Rs[a]?e():Om(e,0)}catch(g){wm(g)}},void 0);
Ps[d]=!0;Qs[a]||(Qs[a]=[]);Qs[a].push(d);return d}return 0}
function Us(a){var b=Ts();b&&(typeof a==="number"?a=[a]:typeof a==="string"&&(a=[parseInt(a,10)]),Sb(a,function(c){b.unsubscribeByKey(c);delete Ps[c]}))}
function Vs(a,b){var c=Ts();c&&c.publish.apply(c,arguments)}
function Ws(a){var b=Ts();if(b)if(b.clear(a),a)Xs(a);else for(var c in Qs)Xs(c)}
function Ts(){return C.ytPubsubPubsubInstance}
function Xs(a){Qs[a]&&(a=Qs[a],Sb(a,function(b){Ps[b]&&delete Ps[b]}),a.length=0)}
M.prototype.subscribe=M.prototype.subscribe;M.prototype.unsubscribeByKey=M.prototype.ac;M.prototype.publish=M.prototype.qb;M.prototype.clear=M.prototype.clear;D("ytPubsubPubsubInstance",Os);D("ytPubsubPubsubTopicToKeys",Qs);D("ytPubsubPubsubIsSynchronous",Rs);D("ytPubsubPubsubSubscribedKeys",Ps);var Ys=Symbol("injectionDeps");function Zs(a){this.name=a}
Zs.prototype.toString=function(){return"InjectionToken("+this.name+")"};
function $s(a){this.key=a}
function at(){this.i=new Map;this.j=new Map;this.h=new Map}
function bt(a,b){a.i.set(b.zc,b);var c=a.j.get(b.zc);if(c)try{c.Rh(a.resolve(b.zc))}catch(d){c.Ph(d)}}
at.prototype.resolve=function(a){return a instanceof $s?ct(this,a.key,[],!0):ct(this,a,[])};
function ct(a,b,c,d){d=d===void 0?!1:d;if(c.indexOf(b)>-1)throw Error("Deps cycle for: "+b);if(a.h.has(b))return a.h.get(b);if(!a.i.has(b)){if(d)return;throw Error("No provider for: "+b);}d=a.i.get(b);c.push(b);if(d.Qd!==void 0)var e=d.Qd;else if(d.Bf)e=d[Ys]?dt(a,d[Ys],c):[],e=d.Bf.apply(d,ra(e));else if(d.Pd){e=d.Pd;var f=e[Ys]?dt(a,e[Ys],c):[];e=new (Function.prototype.bind.apply(e,[null].concat(ra(f))))}else throw Error("Could not resolve providers for: "+b);c.pop();d.Vh||a.h.set(b,e);return e}
function dt(a,b,c){return b?b.map(function(d){return d instanceof $s?ct(a,d.key,c,!0):ct(a,d,c)}):[]}
;var et;function ft(){et||(et=new at);return et}
;var gt=window;function ht(){var a,b;return"h5vcc"in gt&&((a=gt.h5vcc.traceEvent)==null?0:a.traceBegin)&&((b=gt.h5vcc.traceEvent)==null?0:b.traceEnd)?1:"performance"in gt&&gt.performance.mark&&gt.performance.measure?2:0}
function jt(a){var b=ht();switch(b){case 1:gt.h5vcc.traceEvent.traceBegin("YTLR",a);break;case 2:gt.performance.mark(a+"-start");break;case 0:break;default:Db(b,"unknown trace type")}}
function kt(a){var b=ht();switch(b){case 1:gt.h5vcc.traceEvent.traceEnd("YTLR",a);break;case 2:b=a+"-start";var c=a+"-end";gt.performance.mark(c);gt.performance.measure(a,b,c);break;case 0:break;default:Db(b,"unknown trace type")}}
;var lt=R("web_enable_lifecycle_monitoring")&&ht()!==0,mt=R("web_enable_lifecycle_monitoring");function nt(a){var b,c;(c=(b=window).onerror)==null||c.call(b,a.message,"",0,0,a)}
;function ot(a){var b=this;var c=c===void 0?0:c;var d=d===void 0?zo():d;this.j=c;this.scheduler=d;this.i=new yj;this.h=a;for(a={ib:0};a.ib<this.h.length;a={wc:void 0,ib:a.ib},a.ib++)a.wc=this.h[a.ib],c=function(e){return function(){e.wc.Nc();b.h[e.ib].yc=!0;b.h.every(function(f){return f.yc===!0})&&b.i.resolve()}}(a),d=this.getPriority(a.wc),d=this.scheduler.Ra(c,d),this.h[a.ib]=Object.assign({},a.wc,{Nc:c,
jobId:d})}
function pt(a){var b=Array.from(a.h.keys()).sort(function(d,e){return a.getPriority(a.h[e])-a.getPriority(a.h[d])});
b=y(b);for(var c=b.next();!c.done;c=b.next())c=a.h[c.value],c.jobId===void 0||c.yc||(a.scheduler.qa(c.jobId),a.scheduler.Ra(c.Nc,10))}
ot.prototype.cancel=function(){for(var a=y(this.h),b=a.next();!b.done;b=a.next())b=b.value,b.jobId===void 0||b.yc||this.scheduler.qa(b.jobId),b.yc=!0;this.i.resolve()};
ot.prototype.getPriority=function(a){var b;return(b=a.priority)!=null?b:this.j};function qt(a){this.state=a;this.plugins=[];this.o=void 0;this.D={};lt&&jt(this.state)}
r=qt.prototype;r.install=function(a){this.plugins.push(a);return this};
r.uninstall=function(){var a=this;B.apply(0,arguments).forEach(function(b){b=a.plugins.indexOf(b);b>-1&&a.plugins.splice(b,1)})};
r.transition=function(a,b){var c=this;lt&&kt(this.state);var d=this.transitions.find(function(f){return Array.isArray(f.from)?f.from.find(function(g){return g===c.state&&f.to===a}):f.from===c.state&&f.to===a});
if(d){this.j&&(pt(this.j),this.j=void 0);rt(this,a,b);this.state=a;lt&&jt(this.state);d=d.action.bind(this);var e=this.plugins.filter(function(f){return f[a]}).map(function(f){return f[a]});
d(st(this,e),b)}else throw Error("no transition specified from "+this.state+" to "+a);};
function st(a,b){var c=b.filter(function(e){return tt(a,e)===10}),d=b.filter(function(e){return tt(a,e)!==10});
return a.D.Uh?function(){var e=B.apply(0,arguments);return A(function(f){if(f.h==1)return f.yield(a.af.apply(a,[c].concat(ra(e))),2);a.Kd.apply(a,[d].concat(ra(e)));f.h=0})}:function(){var e=B.apply(0,arguments);
a.bf.apply(a,[c].concat(ra(e)));a.Kd.apply(a,[d].concat(ra(e)))}}
r.bf=function(a){for(var b=B.apply(1,arguments),c=zo(),d=y(a),e=d.next(),f={};!e.done;f={Qb:void 0},e=d.next())f.Qb=e.value,c.Jb(function(g){return function(){ut(g.Qb.name);vt(function(){return g.Qb.callback.apply(g.Qb,ra(b))});
wt(g.Qb.name)}}(f))};
r.af=function(a){var b=B.apply(1,arguments),c,d,e,f,g;return A(function(h){h.h==1&&(c=zo(),d=y(a),e=d.next(),f={});if(h.h!=3){if(e.done)return h.A(0);f.Xa=e.value;f.cc=void 0;g=function(k){return function(){ut(k.Xa.name);var l=vt(function(){return k.Xa.callback.apply(k.Xa,ra(b))});
he(l)?k.cc=R("web_lifecycle_error_handling_killswitch")?l.then(function(){wt(k.Xa.name)}):l.then(function(){wt(k.Xa.name)},function(m){nt(m);
wt(k.Xa.name)}):wt(k.Xa.name)}}(f);
c.Jb(g);return f.cc?h.yield(f.cc,3):h.A(3)}f={Xa:void 0,cc:void 0};e=d.next();return h.A(2)})};
r.Kd=function(a){var b=B.apply(1,arguments),c=this,d=a.map(function(e){return{Nc:function(){ut(e.name);vt(function(){return e.callback.apply(e,ra(b))});
wt(e.name)},
priority:tt(c,e)}});
d.length&&(this.j=new ot(d))};
function tt(a,b){var c,d;return(d=(c=a.o)!=null?c:b.priority)!=null?d:0}
function ut(a){lt&&a&&jt(a)}
function wt(a){lt&&a&&kt(a)}
function rt(a,b,c){mt&&console.groupCollapsed&&console.groupEnd&&(console.groupCollapsed("["+a.constructor.name+"] '"+a.state+"' to '"+b+"'"),console.log("with message: ",c),console.groupEnd())}
fa.Object.defineProperties(qt.prototype,{currentState:{configurable:!0,enumerable:!0,get:function(){return this.state}}});
function vt(a){if(R("web_lifecycle_error_handling_killswitch"))return a();try{return a()}catch(b){nt(b)}}
;function xt(a){qt.call(this,a===void 0?"none":a);this.h=null;this.o=10;this.transitions=[{from:"none",to:"application_navigating",action:this.i},{from:"application_navigating",to:"none",action:this.u},{from:"application_navigating",to:"application_navigating",action:function(){}},
{from:"none",to:"none",action:function(){}}]}
var zt;w(xt,qt);xt.prototype.i=function(a,b){var c=this;this.h=Un(function(){c.currentState==="application_navigating"&&c.transition("none")},5E3);
a(b==null?void 0:b.event)};
xt.prototype.u=function(a,b){this.h&&(ak.qa(this.h),this.h=null);a(b==null?void 0:b.event)};
function At(){zt||(zt=new xt);return zt}
;var Bt=[];D("yt.logging.transport.getScrapedGelPayloads",function(){return Bt});function Ct(){this.store={};this.h={}}
Ct.prototype.storePayload=function(a,b){a=Dt(a);this.store[a]?this.store[a].push(b):(this.h={},this.store[a]=[b]);R("more_accurate_gel_parser")&&(b=new CustomEvent("TRANSPORTING_NEW_EVENT"),window.dispatchEvent(b));return a};
Ct.prototype.smartExtractMatchingEntries=function(a){if(!a.keys.length)return[];for(var b=Et(this,a.keys.splice(0,1)[0]),c=[],d=0;d<b.length;d++)this.store[b[d]]&&a.sizeLimit&&(this.store[b[d]].length<=a.sizeLimit?(c.push.apply(c,ra(this.store[b[d]])),delete this.store[b[d]]):c.push.apply(c,ra(this.store[b[d]].splice(0,a.sizeLimit))));(a==null?0:a.sizeLimit)&&c.length<(a==null?void 0:a.sizeLimit)&&(a.sizeLimit-=c.length,c.push.apply(c,ra(this.smartExtractMatchingEntries(a))));return c};
Ct.prototype.extractMatchingEntries=function(a){a=Et(this,a);for(var b=[],c=0;c<a.length;c++)this.store[a[c]]&&(b.push.apply(b,ra(this.store[a[c]])),delete this.store[a[c]]);return b};
Ct.prototype.getSequenceCount=function(a){a=Et(this,a);for(var b=0,c=0;c<a.length;c++){var d=void 0;b+=((d=this.store[a[c]])==null?void 0:d.length)||0}return b};
function Et(a,b){var c=Dt(b);if(a.h[c])return a.h[c];var d=Object.keys(a.store)||[];if(d.length<=1&&Dt(b)===d[0])return d;for(var e=[],f=0;f<d.length;f++){var g=d[f].split("/");if(Ft(b.auth,g[0])){var h=b.isJspb;Ft(h===void 0?"undefined":h?"true":"false",g[1])&&Ft(b.cttAuthInfo,g[2])&&(h=b.tier,h=h===void 0?"undefined":JSON.stringify(h),Ft(h,g[3])&&e.push(d[f]))}}return a.h[c]=e}
function Ft(a,b){return a===void 0||a==="undefined"?!0:a===b}
Ct.prototype.getSequenceCount=Ct.prototype.getSequenceCount;Ct.prototype.extractMatchingEntries=Ct.prototype.extractMatchingEntries;Ct.prototype.smartExtractMatchingEntries=Ct.prototype.smartExtractMatchingEntries;Ct.prototype.storePayload=Ct.prototype.storePayload;function Dt(a){return[a.auth===void 0?"undefined":a.auth,a.isJspb===void 0?"undefined":a.isJspb,a.cttAuthInfo===void 0?"undefined":a.cttAuthInfo,a.tier===void 0?"undefined":a.tier].join("/")}
;function Gt(a,b){if(a)return a[b.name]}
;var Ht=S("initial_gel_batch_timeout",2E3),It=S("gel_queue_timeout_max_ms",6E4),Jt=S("gel_min_batch_size",5),Kt=void 0;function Lt(){this.o=this.h=this.i=0;this.j=!1}
var Mt=new Lt,Nt=new Lt,Ot=new Lt,Pt=new Lt,Qt,Rt=!0,St=C.ytLoggingTransportTokensToCttTargetIds_||{};D("ytLoggingTransportTokensToCttTargetIds_",St);var Tt={};function Ut(){var a=E("yt.logging.ims");a||(a=new Ct,D("yt.logging.ims",a));return a}
function Vt(a,b){if(a.endpoint==="log_event"){Wt();var c=Xt(a),d=Yt(a.payload)||"";a:{if(R("enable_web_tiered_gel")){var e=Pr[d||""];var f,g,h,k=ft().resolve(new $s(sq))==null?void 0:(f=tq())==null?void 0:(g=f.loggingHotConfig)==null?void 0:(h=g.eventLoggingConfig)==null?void 0:h.payloadPolicies;if(k)for(f=0;f<k.length;f++)if(k[f].payloadNumber===e){e=k[f];break a}}e=void 0}k=200;if(e){if(e.enabled===!1&&!R("web_payload_policy_disabled_killswitch"))return;k=Zt(e.tier);if(k===400){$t(a,b);return}}Tt[c]=
!0;c={cttAuthInfo:c,isJspb:!1,tier:k};Ut().storePayload(c,a.payload);au(b,c,d==="gelDebuggingEvent")}}
function au(a,b,c){function d(){bu({writeThenSend:!0},void 0,e,b.tier)}
var e=!1;e=e===void 0?!1:e;c=c===void 0?!1:c;a&&(Kt=new a);a=S("tvhtml5_logging_max_batch_ads_fork")||S("tvhtml5_logging_max_batch")||S("web_logging_max_batch")||100;var f=U(),g=cu(e,b.tier),h=g.o;c&&(g.j=!0);c=0;b&&(c=Ut().getSequenceCount(b));c>=1E3?d():c>=a?Qt||(Qt=du(function(){d();Qt=void 0},0)):f-h>=10&&(eu(e,b.tier),g.o=f)}
function $t(a,b){if(a.endpoint==="log_event"){R("more_accurate_gel_parser")&&Ut().storePayload({isJspb:!1},a.payload);Wt();var c=Xt(a),d=new Map;d.set(c,[a.payload]);var e=Yt(a.payload)||"";b&&(Kt=new b);return new ui(function(f,g){Kt&&Kt.isReady()?fu(d,Kt,f,g,{bypassNetworkless:!0},!0,e==="gelDebuggingEvent"):f()})}}
function Xt(a){var b="";if(a.dangerousLogToVisitorSession)b="visitorOnlyApprovedKey";else if(a.cttAuthInfo){b=a.cttAuthInfo;var c={};b.videoId?c.videoId=b.videoId:b.playlistId&&(c.playlistId=b.playlistId);St[a.cttAuthInfo.token]=c;b=a.cttAuthInfo.token}return b}
function bu(a,b,c,d){a=a===void 0?{}:a;c=c===void 0?!1:c;new ui(function(e,f){var g=cu(c,d),h=g.j;g.j=!1;gu(g.i);gu(g.h);g.h=0;Kt&&Kt.isReady()?d===void 0&&R("enable_web_tiered_gel")?hu(e,f,a,b,c,300,h):hu(e,f,a,b,c,d,h):(eu(c,d),e())})}
function hu(a,b,c,d,e,f,g){var h=Kt;c=c===void 0?{}:c;e=e===void 0?!1:e;f=f===void 0?200:f;g=g===void 0?!1:g;var k=new Map,l={isJspb:e,cttAuthInfo:d,tier:f};e={isJspb:e,cttAuthInfo:d};if(d!==void 0)f=R("enable_web_tiered_gel")?Ut().smartExtractMatchingEntries({keys:[l,e],sizeLimit:1E3}):Ut().extractMatchingEntries(e),k.set(d,f);else for(d=y(Object.keys(Tt)),l=d.next();!l.done;l=d.next())l=l.value,e=R("enable_web_tiered_gel")?Ut().smartExtractMatchingEntries({keys:[{isJspb:!1,cttAuthInfo:l,tier:f},
{isJspb:!1,cttAuthInfo:l}],sizeLimit:1E3}):Ut().extractMatchingEntries({isJspb:!1,cttAuthInfo:l}),e.length>0&&k.set(l,e),(R("web_fp_via_jspb_and_json")&&c.writeThenSend||!R("web_fp_via_jspb_and_json"))&&delete Tt[l];fu(k,h,a,b,c,!1,g)}
function eu(a,b){function c(){bu({writeThenSend:!0},void 0,a,b)}
a=a===void 0?!1:a;b=b===void 0?200:b;var d=cu(a,b),e=d===Pt||d===Ot?5E3:It;R("web_gel_timeout_cap")&&!d.h&&(e=du(function(){c()},e),d.h=e);
gu(d.i);e=P("LOGGING_BATCH_TIMEOUT",S("web_gel_debounce_ms",1E4));R("shorten_initial_gel_batch_timeout")&&Rt&&(e=Ht);e=du(function(){S("gel_min_batch_size")>0?Ut().getSequenceCount({cttAuthInfo:void 0,isJspb:a,tier:b})>=Jt&&c():c()},e);
d.i=e}
function fu(a,b,c,d,e,f,g){e=e===void 0?{}:e;var h=Math.round(U()),k=a.size,l=(g===void 0?0:g)&&R("vss_through_gel_video_stats")?"video_stats":"log_event";a=y(a);var m=a.next();for(g={};!m.done;g={Tc:void 0,batchRequest:void 0,dangerousLogToVisitorSession:void 0,Wc:void 0,Vc:void 0},m=a.next()){var n=y(m.value);m=n.next().value;n=n.next().value;g.batchRequest=Bg({context:zq(b.config_||yq())});if(!Na(n)&&!R("throw_err_when_logevent_malformed_killswitch")){d();break}g.batchRequest.events=n;(n=St[m])&&
iu(g.batchRequest,m,n);delete St[m];g.dangerousLogToVisitorSession=m==="visitorOnlyApprovedKey";ju(g.batchRequest,h,g.dangerousLogToVisitorSession);R("always_send_and_write")&&(e.writeThenSend=!1);g.Wc=function(p){R("start_client_gcf")&&ak.ma(function(){return A(function(t){return t.yield(ku(p),0)})});
k--;k||c()};
g.Tc=0;g.Vc=function(p){return function(){p.Tc++;if(e.bypassNetworkless&&p.Tc===1)try{tr(b,l,p.batchRequest,lu({writeThenSend:!0},p.dangerousLogToVisitorSession,p.Wc,p.Vc,f)),Rt=!1}catch(t){wm(t),d()}k--;k||c()}}(g);
try{tr(b,l,g.batchRequest,lu(e,g.dangerousLogToVisitorSession,g.Wc,g.Vc,f)),Rt=!1}catch(p){wm(p),d()}}}
function lu(a,b,c,d,e){a={retry:!0,onSuccess:c,onError:d,networklessOptions:a,dangerousLogToVisitorSession:b,uh:!!e,headers:{},postBodyFormat:"",postBody:"",compress:R("compress_gel")||R("compress_gel_lr")};mu()&&(a.headers["X-Goog-Request-Time"]=JSON.stringify(Math.round(U())));return a}
function ju(a,b,c){mu()||(a.requestTimeMs=String(b));R("unsplit_gel_payloads_in_logs")&&(a.unsplitGelPayloadsInLogs=!0);!c&&(b=P("EVENT_ID"))&&((c=P("BATCH_CLIENT_COUNTER")||0)||(c=Math.floor(Math.random()*65535/2)),c++,c>65535&&(c=1),qm("BATCH_CLIENT_COUNTER",c),a.serializedClientEventId={serializedEventId:b,clientCounter:String(c)})}
function iu(a,b,c){if(c.videoId)var d="VIDEO";else if(c.playlistId)d="PLAYLIST";else return;a.credentialTransferTokenTargetId=c;a.context=a.context||{};a.context.user=a.context.user||{};a.context.user.credentialTransferTokens=[{token:b,scope:d}]}
function Wt(){var a;(a=E("yt.logging.transport.enableScrapingForTest"))||(a=Qm("il_payload_scraping"),a=(a!==void 0?String(a):"")!=="enable_il_payload_scraping");a||(Bt=[],D("yt.logging.transport.enableScrapingForTest",!0),D("yt.logging.transport.scrapedPayloadsForTesting",Bt),D("yt.logging.transport.payloadToScrape","visualElementShown visualElementHidden visualElementAttached screenCreated visualElementGestured visualElementStateChanged".split(" ")),D("yt.logging.transport.getScrapedPayloadFromClientEventsFunction"),
D("yt.logging.transport.scrapeClientEvent",!0))}
function mu(){return R("use_request_time_ms_header")||R("lr_use_request_time_ms_header")}
function du(a,b){return R("transport_use_scheduler")===!1?Om(a,b):R("logging_avoid_blocking_during_navigation")||R("lr_logging_avoid_blocking_during_navigation")?Un(function(){if(At().currentState==="none")a();else{var c={};At().install((c.none={callback:a},c))}},b):Un(a,b)}
function gu(a){R("transport_use_scheduler")?ak.qa(a):window.clearTimeout(a)}
function ku(a){var b,c,d,e,f,g,h,k,l,m;return A(function(n){return n.h==1?(d=(b=a)==null?void 0:(c=b.responseContext)==null?void 0:c.globalConfigGroup,e=Gt(d,Tl),g=(f=d)==null?void 0:f.hotHashData,h=Gt(d,Sl),l=(k=d)==null?void 0:k.coldHashData,(m=ft().resolve(new $s(sq)))?g?e?n.yield(uq(m,g,e),2):n.yield(uq(m,g),2):n.A(2):n.return()):l?h?n.yield(vq(m,l,h),0):n.yield(vq(m,l),0):n.A(0)})}
function cu(a,b){b=b===void 0?200:b;return a?b===300?Pt:Nt:b===300?Ot:Mt}
function Yt(a){a=Object.keys(a);a=y(a);for(var b=a.next();!b.done;b=a.next())if(b=b.value,Pr[b])return b}
function Zt(a){switch(a){case "DELAYED_EVENT_TIER_UNSPECIFIED":return 0;case "DELAYED_EVENT_TIER_DEFAULT":return 100;case "DELAYED_EVENT_TIER_DISPATCH_TO_EMPTY":return 200;case "DELAYED_EVENT_TIER_FAST":return 300;case "DELAYED_EVENT_TIER_IMMEDIATE":return 400;default:return 200}}
;var nu=C.ytLoggingGelSequenceIdObj_||{};D("ytLoggingGelSequenceIdObj_",nu);
function ou(a,b,c,d){d=d===void 0?{}:d;var e={},f=Math.round(d.timestamp||U());e.eventTimeMs=f<Number.MAX_SAFE_INTEGER?f:0;e[a]=b;a=Ns();e.context={lastActivityMs:String(d.timestamp||!isFinite(a)?-1:a)};d.sequenceGroup&&!R("web_gel_sequence_info_killswitch")&&(a=e.context,b=d.sequenceGroup,nu[b]=b in nu?nu[b]+1:0,a.sequence={index:nu[b],groupKey:b},d.endOfSequence&&delete nu[d.sequenceGroup]);(d.sendIsolatedPayload?$t:Vt)({endpoint:"log_event",payload:e,cttAuthInfo:d.cttAuthInfo,dangerousLogToVisitorSession:d.dangerousLogToVisitorSession},
c)}
;function Jo(a,b,c){c=c===void 0?{}:c;var d=ts;P("ytLoggingEventsDefaultDisabled",!1)&&ts===ts&&(d=null);ou(a,b,d,c)}
;function pu(a){return a.slice(0,void 0).map(function(b){return b.name}).join(" > ")}
;var qu=new Set,ru=0,su=0,tu=0,uu=[],vu=["PhantomJS","Googlebot","TO STOP THIS SECURITY SCAN go/scan"];function Io(a){wu(a)}
function V(a){wu(a,"WARNING")}
function xu(a){a instanceof Error?wu(a):(a=Sa(a)?JSON.stringify(a):String(a),a=new T(a),a.name="RejectedPromiseError",V(a))}
function wu(a,b,c,d,e,f,g,h){f=f===void 0?{}:f;f.name=c||P("INNERTUBE_CONTEXT_CLIENT_NAME",1);f.version=d||P("INNERTUBE_CONTEXT_CLIENT_VERSION");c=f;b=b===void 0?"ERROR":b;g=g===void 0?!1:g;b=b===void 0?"ERROR":b;g=g===void 0?!1:g;if(a&&(a.hasOwnProperty("level")&&a.level&&(b=a.level),R("console_log_js_exceptions")&&(d=[],d.push("Name: "+a.name),d.push("Message: "+a.message),a.hasOwnProperty("params")&&d.push("Error Params: "+JSON.stringify(a.params)),a.hasOwnProperty("args")&&d.push("Error args: "+
JSON.stringify(a.args)),d.push("File name: "+a.fileName),d.push("Stacktrace: "+a.stack),d=d.join("\n"),window.console.log(d,a)),!(ru>=5))){d=uu;var k=$b(a);e=k.message||"Unknown Error";f=k.name||"UnknownError";var l=k.stack||a.i||"Not available";if(l.startsWith(f+": "+e)){var m=l.split("\n");m.shift();l=m.join("\n")}m=k.lineNumber||"Not available";k=k.fileName||"Not available";var n=0;if(a.hasOwnProperty("args")&&a.args&&a.args.length)for(var p=0;p<a.args.length&&!(n=rn(a.args[p],"params."+p,c,n),
n>=500);p++);else if(a.hasOwnProperty("params")&&a.params){var t=a.params;if(typeof a.params==="object")for(p in t){if(t[p]){var u="params."+p,x=tn(t[p]);c[u]=x;n+=u.length+x.length;if(n>500)break}}else c.params=tn(t)}if(d.length)for(p=0;p<d.length&&!(n=rn(d[p],"params.context."+p,c,n),n>=500);p++);navigator.vendor&&!c.hasOwnProperty("vendor")&&(c["device.vendor"]=navigator.vendor);p={message:e,name:f,lineNumber:m,fileName:k,stack:l,params:c,sampleWeight:1};c=Number(a.columnNumber);isNaN(c)||(p.lineNumber=
p.lineNumber+":"+c);if(a.level==="IGNORED")a=0;else a:{a=nn();c=y(a.Ya);for(d=c.next();!d.done;d=c.next())if(d=d.value,p.message&&p.message.match(d.Kh)){a=d.weight;break a}a=y(a.Ta);for(c=a.next();!c.done;c=a.next())if(c=c.value,c.callback(p)){a=c.weight;break a}a=1}p.sampleWeight=a;a=y(hn);for(c=a.next();!c.done;c=a.next())if(c=c.value,c.vc[p.name])for(e=y(c.vc[p.name]),d=e.next();!d.done;d=e.next())if(f=d.value,d=p.message.match(f.regexp)){p.params["params.error.original"]=d[0];e=f.groups;f={};
for(m=0;m<e.length;m++)f[e[m]]=d[m+1],p.params["params.error."+e[m]]=d[m+1];p.message=c.Rc(f);break}p.params||(p.params={});a=nn();p.params["params.errorServiceSignature"]="msg="+a.Ya.length+"&cb="+a.Ta.length;p.params["params.serviceWorker"]="false";C.document&&C.document.querySelectorAll&&(p.params["params.fscripts"]=String(document.querySelectorAll("script:not([nonce])").length));(new Eg(Fg,"sample")).constructor!==Eg&&(p.params["params.fconst"]="true");window.yterr&&typeof window.yterr==="function"&&
window.yterr(p);if(p.sampleWeight!==0&&!qu.has(p.message)){if(g&&R("web_enable_error_204"))yu(b===void 0?"ERROR":b,p);else{b=b===void 0?"ERROR":b;b==="ERROR"?(on.qb("handleError",p),R("record_app_crashed_web")&&tu===0&&p.sampleWeight===1&&(tu++,g={appCrashType:"APP_CRASH_TYPE_BREAKPAD"},R("report_client_error_with_app_crash_ks")||(g.systemHealth={crashData:{clientError:{logMessage:{message:p.message}}}}),Jo("appCrashed",g)),su++):b==="WARNING"&&on.qb("handleWarning",p);if(R("kevlar_gel_error_routing")){g=
b;h=h===void 0?{}:h;b:{a=y(vu);for(c=a.next();!c.done;c=a.next())if(Po(c.value.toLowerCase())){a=!0;break b}a=!1}if(a)h=void 0;else{c={stackTrace:p.stack};p.fileName&&(c.filename=p.fileName);a=p.lineNumber&&p.lineNumber.split?p.lineNumber.split(":"):[];a.length!==0&&(a.length!==1||isNaN(Number(a[0]))?a.length!==2||isNaN(Number(a[0]))||isNaN(Number(a[1]))||(c.lineNumber=Number(a[0]),c.columnNumber=Number(a[1])):c.lineNumber=Number(a[0]));a={level:"ERROR_LEVEL_UNKNOWN",message:p.message,errorClassName:p.name,
sampleWeight:p.sampleWeight};g==="ERROR"?a.level="ERROR_LEVEL_ERROR":g==="WARNING"&&(a.level="ERROR_LEVEL_WARNNING");c={isObfuscated:!0,browserStackInfo:c};h.pageUrl=window.location.href;h.kvPairs=[];P("FEXP_EXPERIMENTS")&&(h.experimentIds=P("FEXP_EXPERIMENTS"));d=P("LATEST_ECATCHER_SERVICE_TRACKING_PARAMS");if(!rm("web_disable_gel_stp_ecatcher_killswitch")&&d)for(e=y(Object.keys(d)),f=e.next();!f.done;f=e.next())f=f.value,h.kvPairs.push({key:f,value:String(d[f])});if(d=p.params)for(e=y(Object.keys(d)),
f=e.next();!f.done;f=e.next())f=f.value,h.kvPairs.push({key:"client."+f,value:String(d[f])});d=P("SERVER_NAME");e=P("SERVER_VERSION");d&&e&&(h.kvPairs.push({key:"server.name",value:d}),h.kvPairs.push({key:"server.version",value:e}));h={errorMetadata:h,stackTrace:c,logMessage:a}}h&&(Jo("clientError",h),(g==="ERROR"||R("errors_flush_gel_always_killswitch"))&&bu(void 0,void 0,!1))}R("suppress_error_204_logging")||yu(b,p)}try{qu.add(p.message)}catch(z){}ru++}}}
function yu(a,b){var c=b.params||{};a={urlParams:{a:"logerror",t:"jserror",type:b.name,msg:b.message.substr(0,250),line:b.lineNumber,level:a,"client.name":c.name},postParams:{url:P("PAGE_NAME",window.location.href),file:b.fileName},method:"POST"};c.version&&(a["client.version"]=c.version);if(a.postParams){b.stack&&(a.postParams.stack=b.stack);b=y(Object.keys(c));for(var d=b.next();!d.done;d=b.next())d=d.value,a.postParams["client."+d]=c[d];if(c=P("LATEST_ECATCHER_SERVICE_TRACKING_PARAMS"))for(b=y(Object.keys(c)),
d=b.next();!d.done;d=b.next())d=d.value,a.postParams[d]=c[d];c=P("SERVER_NAME");b=P("SERVER_VERSION");c&&b&&(a.postParams["server.name"]=c,a.postParams["server.version"]=b)}Zm(P("ECATCHER_REPORT_HOST","")+"/error_204",a)}
function zu(a){var b=B.apply(1,arguments);a.args||(a.args=[]);a.args.push.apply(a.args,ra(b))}
;function Au(){this.register=new Map}
function Bu(a){a=y(a.register.values());for(var b=a.next();!b.done;b=a.next())b.value.Oh("ABORTED")}
Au.prototype.clear=function(){Bu(this);this.register.clear()};
var Cu=new Au;var Du=Date.now().toString();
function Eu(){a:{if(window.crypto&&window.crypto.getRandomValues)try{var a=Array(16),b=new Uint8Array(16);window.crypto.getRandomValues(b);for(var c=0;c<a.length;c++)a[c]=b[c];var d=a;break a}catch(e){}d=Array(16);for(a=0;a<16;a++){b=Date.now();for(c=0;c<b%23;c++)d[a]=Math.random();d[a]=Math.floor(Math.random()*256)}if(Du)for(a=1,b=0;b<Du.length;b++)d[a%16]=d[a%16]^d[(a-1)%16]/4^Du.charCodeAt(b),a++}a=[];for(b=0;b<d.length;b++)a.push("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_".charAt(d[b]&63));
return a.join("")}
;var Fu,Gu=C.ytLoggingDocDocumentNonce_;Gu||(Gu=Eu(),D("ytLoggingDocDocumentNonce_",Gu));Fu=Gu;function Hu(a){this.h=a}
r=Hu.prototype;r.getAsJson=function(){var a={};this.h.trackingParams!==void 0?a.trackingParams=this.h.trackingParams:(a.veType=this.h.veType,this.h.veCounter!==void 0&&(a.veCounter=this.h.veCounter),this.h.elementIndex!==void 0&&(a.elementIndex=this.h.elementIndex));this.h.dataElement!==void 0&&(a.dataElement=this.h.dataElement.getAsJson());this.h.youtubeData!==void 0&&(a.youtubeData=this.h.youtubeData);this.h.isCounterfactual&&(a.isCounterfactual=!0);return a};
r.getAsJspb=function(){var a=new Vl;this.h.trackingParams!==void 0?a.setTrackingParams(this.h.trackingParams):(this.h.veType!==void 0&&gf(a,2,He(this.h.veType)),this.h.veCounter!==void 0&&gf(a,6,He(this.h.veCounter)),this.h.elementIndex!==void 0&&gf(a,3,He(this.h.elementIndex)),this.h.isCounterfactual&&gf(a,5,De(!0)));if(this.h.dataElement!==void 0){var b=this.h.dataElement.getAsJspb();uf(a,Vl,7,b)}this.h.youtubeData!==void 0&&uf(a,Ul,8,this.h.jspbYoutubeData);return a};
r.toString=function(){return JSON.stringify(this.getAsJson())};
r.isClientVe=function(){return!this.h.trackingParams&&!!this.h.veType};
r.getLoggingDirectives=function(){return this.h.loggingDirectives};function Iu(a){return P("client-screen-nonce-store",{})[a===void 0?0:a]}
function Ju(a,b){b=b===void 0?0:b;var c=P("client-screen-nonce-store");c||(c={},qm("client-screen-nonce-store",c));c[b]=a}
function Ku(a){a=a===void 0?0:a;return a===0?"ROOT_VE_TYPE":"ROOT_VE_TYPE."+a}
function Lu(a){return P(Ku(a===void 0?0:a))}
D("yt_logging_screen.getRootVeType",Lu);function Mu(){var a=P("csn-to-ctt-auth-info");a||(a={},qm("csn-to-ctt-auth-info",a));return a}
function Nu(){return Object.values(P("client-screen-nonce-store",{})).filter(function(a){return a!==void 0})}
function Ou(a){a=Iu(a===void 0?0:a);if(!a&&!P("USE_CSN_FALLBACK",!0))return null;a||(a="UNDEFINED_CSN");return a?a:null}
D("yt_logging_screen.getCurrentCsn",Ou);function Pu(a,b,c){var d=Mu();(c=Ou(c))&&delete d[c];b&&(d[a]=b)}
function Qu(a){return Mu()[a]}
D("yt_logging_screen.getCttAuthInfo",Qu);D("yt_logging_screen.setCurrentScreen",function(a,b,c,d){c=c===void 0?0:c;if(a!==Iu(c)||b!==P(Ku(c)))if(Pu(a,d,c),Ju(a,c),qm(Ku(c),b),b=function(){setTimeout(function(){a&&Jo("foregroundHeartbeatScreenAssociated",{clientDocumentNonce:Fu,clientScreenNonce:a})},0)},"requestAnimationFrame"in window)try{window.requestAnimationFrame(b)}catch(e){b()}else b()});function Ru(){var a=Ag(Su),b;return(new ui(function(c,d){a.onSuccess=function(e){Nm(e)?c(new Tu(e)):d(new Uu("Request failed, status="+(e&&"status"in e?e.status:-1),"net.badstatus",e))};
a.onError=function(e){d(new Uu("Unknown request error","net.unknown",e))};
a.onTimeout=function(e){d(new Uu("Request timed out","net.timeout",e))};
b=Zm("//googleads.g.doubleclick.net/pagead/id",a)})).Dc(function(c){if(c instanceof Di){var d;
(d=b)==null||d.abort()}return zi(c)})}
function Uu(a,b,c){db.call(this,a+", errorCode="+b);this.errorCode=b;this.xhr=c;this.name="PromiseAjaxError"}
w(Uu,db);function Tu(a){this.xhr=a}
;function Vu(){this.X=0;this.h=null}
Vu.prototype.then=function(a,b,c){return this.X===1&&a?(a=a.call(c,this.h))&&typeof a.then==="function"?a:Wu(a):this.X===2&&b?(a=b.call(c,this.h))&&typeof a.then==="function"?a:Xu(a):this};
Vu.prototype.getValue=function(){return this.h};
Vu.prototype.isRejected=function(){return this.X==2};
Vu.prototype.$goog_Thenable=!0;function Xu(a){var b=new Vu;a=a===void 0?null:a;b.X=2;b.h=a===void 0?null:a;return b}
function Wu(a){var b=new Vu;a=a===void 0?null:a;b.X=1;b.h=a===void 0?null:a;return b}
;function Yu(a){var b=P("INNERTUBE_HOST_OVERRIDE");b&&(a=String(b)+String(jc(a)));return a}
function Zu(a){var b={};R("json_condensed_response")&&(b.prettyPrint="false");return a=Hm(a,b||{},!1)}
function $u(a,b){var c=c===void 0?{}:c;a={method:b===void 0?"POST":b,mode:Im(a)?"same-origin":"cors",credentials:Im(a)?"same-origin":"include"};b={};for(var d=y(Object.keys(c)),e=d.next();!e.done;e=d.next())e=e.value,c[e]&&(b[e]=c[e]);Object.keys(b).length>0&&(a.headers=b);return a}
;function av(){return jg()||(hd||id)&&Po("applewebkit")&&!Po("version")&&(!Po("safari")||Po("gsa/"))||gd&&Po("version/")?!0:P("EOM_VISITOR_DATA")?!1:!0}
;function bv(a){a:{var b="EMBEDDED_PLAYER_MODE_UNKNOWN";window.location.hostname.includes("youtubeeducation.com")&&(b="EMBEDDED_PLAYER_MODE_PFL");var c=a.raw_embedded_player_response;if(!c&&(a=a.embedded_player_response))try{c=JSON.parse(a)}catch(e){break a}if(c)b:for(var d in Zl)if(Zl[d]==c.embeddedPlayerMode){b=Zl[d];break b}}return b==="EMBEDDED_PLAYER_MODE_PFL"}
;function cv(a){db.call(this,a.message||a.description||a.name);this.isMissing=a instanceof dv;this.isTimeout=a instanceof Uu&&a.errorCode=="net.timeout";this.isCanceled=a instanceof Di}
w(cv,db);cv.prototype.name="BiscottiError";function dv(){db.call(this,"Biscotti ID is missing from server")}
w(dv,db);dv.prototype.name="BiscottiMissingError";var Su={format:"RAW",method:"GET",timeout:5E3,withCredentials:!0},ev=null;function fv(){if(R("disable_biscotti_fetch_entirely_for_all_web_clients"))return Error("Biscotti id fetching has been disabled entirely.");if(!av())return Error("User has not consented - not fetching biscotti id.");var a=P("PLAYER_VARS",{});if(yg(a)=="1")return Error("Biscotti ID is not available in private embed mode");if(bv(a))return Error("Biscotti id fetching has been disabled for pfl.")}
function jm(){var a=fv();if(a!==void 0)return zi(a);ev||(ev=Ru().then(gv).Dc(function(b){return hv(2,b)}));
return ev}
function gv(a){a=a.xhr.responseText;if(a.lastIndexOf(")]}'",0)!=0)throw new dv;a=JSON.parse(a.substr(4));if((a.type||1)>1)throw new dv;a=a.id;km(a);ev=Wu(a);iv(18E5,2);return a}
function hv(a,b){b=new cv(b);km("");ev=Xu(b);a>0&&iv(12E4,a-1);throw b;}
function iv(a,b){Om(function(){Ru().then(gv,function(c){return hv(b,c)}).Dc(si)},a)}
function jv(){try{var a=E("yt.ads.biscotti.getId_");return a?a():jm()}catch(b){return zi(b)}}
;var Mb=sa(["data-"]);function kv(a){a&&(a.dataset?a.dataset[lv()]="true":Nb(a))}
function mv(a){return a?a.dataset?a.dataset[lv()]:a.getAttribute("data-loaded"):null}
var nv={};function lv(){return nv.loaded||(nv.loaded="loaded".replace(/\-([a-z])/g,function(a,b){return b.toUpperCase()}))}
;function ov(a){a=a||{};var b={},c={};this.url=a.url||"";this.args=a.args||Ag(b);this.assets=a.assets||{};this.attrs=a.attrs||Ag(c);this.fallback=a.fallback||null;this.fallbackMessage=a.fallbackMessage||null;this.html5=!!a.html5;this.disable=a.disable||{};this.loaded=!!a.loaded;this.messages=a.messages||{}}
ov.prototype.clone=function(){var a=new ov,b;for(b in this)if(this.hasOwnProperty(b)){var c=this[b];Ma(c)=="object"?a[b]=Ag(c):a[b]=c}return a};var pv=["att/get"],qv=["share/get_share_panel"],rv=["share/get_web_player_share_panel"],sv=["feedback"],tv=["notification/modify_channel_preference"],uv=["browse/edit_playlist"],vv=["subscription/subscribe"],wv=["subscription/unsubscribe"];var xv=window.yt&&window.yt.msgs_||window.ytcfg&&window.ytcfg.msgs||{};D("yt.msgs_",xv);function yv(a){lm(xv,arguments)}
;function zv(a,b,c){Av(a,b,c===void 0?null:c)}
function Bv(a){a=Cv(a);var b=document.getElementById(a);b&&(Ws(a),b.parentNode.removeChild(b))}
function Dv(a,b){a&&b&&(a=""+Ta(b),(a=Ev[a])&&Us(a))}
function Av(a,b,c){c=c===void 0?null:c;var d=Cv(a),e=document.getElementById(d),f=e&&mv(e),g=e&&!f;f?b&&b():(b&&(f=Ss(d,b),b=""+Ta(b),Ev[b]=f),g||(e=Fv(a,d,function(){mv(e)||(kv(e),Vs(d),Om(function(){Ws(d)},0))},c)))}
function Fv(a,b,c,d){d=d===void 0?null:d;var e=Hg("SCRIPT");e.id=b;e.onload=function(){c&&setTimeout(c,0)};
e.onreadystatechange=function(){switch(e.readyState){case "loaded":case "complete":e.onload()}};
d&&e.setAttribute("nonce",d);Kb(e,Ql(a));a=document.getElementsByTagName("head")[0]||document.body;a.insertBefore(e,a.firstChild);return e}
function Cv(a){var b=document.createElement("a");Cb(b,a);a=b.href.replace(/^[a-zA-Z]+:\/\//,"//");return"js-"+dc(a)}
var Ev={};function Gv(a){var b=Hv(a),c=document.getElementById(b),d=c&&mv(c);d||c&&!d||(c=Iv(a,b,function(){if(!mv(c)){kv(c);Vs(b);var e=$a(Ws,b);Om(e,0)}}))}
function Iv(a,b,c){var d=document.createElement("link");d.id=b;d.onload=function(){c&&setTimeout(c,0)};
a=Ql(a);Qb(d,a);(document.getElementsByTagName("head")[0]||document.body).appendChild(d);return d}
function Hv(a){var b=Hg("A");Cb(b,new vb(a));a=b.href.replace(/^[a-zA-Z]+:\/\//,"//");return"css-"+dc(a)}
;function Jv(a){var b=B.apply(1,arguments);if(!Kv(a)||b.some(function(d){return!Kv(d)}))throw Error("Only objects may be merged.");
b=y(b);for(var c=b.next();!c.done;c=b.next())Lv(a,c.value)}
function Lv(a,b){for(var c in b)if(Kv(b[c])){if(c in a&&!Kv(a[c]))throw Error("Cannot merge an object into a non-object.");c in a||(a[c]={});Lv(a[c],b[c])}else if(Mv(b[c])){if(c in a&&!Mv(a[c]))throw Error("Cannot merge an array into a non-array.");c in a||(a[c]=[]);Nv(a[c],b[c])}else a[c]=b[c];return a}
function Nv(a,b){b=y(b);for(var c=b.next();!c.done;c=b.next())c=c.value,Kv(c)?a.push(Lv({},c)):Mv(c)?a.push(Nv([],c)):a.push(c);return a}
function Kv(a){return typeof a==="object"&&!Array.isArray(a)}
function Mv(a){return typeof a==="object"&&Array.isArray(a)}
;var Ov="absolute_experiments app conditional_experiments debugcss debugjs expflag forced_experiments pbj pbjreload sbb spf spfreload sr_bns_address sttick".split(" ");
function Pv(a,b){var c=c===void 0?!0:c;var d=P("VALID_SESSION_TEMPDATA_DOMAINS",[]),e=ic(window.location.href);e&&d.push(e);e=ic(a);if(Rb(d,e)>=0||!e&&a.lastIndexOf("/",0)==0)if(d=document.createElement("a"),Cb(d,a),a=d.href)if(a=jc(a),a=kc(a))if(c&&!b.csn&&(b.itct||b.ved)&&(b=Object.assign({csn:Ou()},b)),f){var f=parseInt(f,10);isFinite(f)&&f>0&&Qv(a,b,f)}else Qv(a,b)}
function Qv(a,b,c){a=Rv(a);b=b?mc(b):"";c=c||5;av()&&An(a,b,c)}
function Rv(a){for(var b=y(Ov),c=b.next();!c.done;c=b.next())a=rc(a,c.value);return"ST-"+dc(a).toString(36)}
;function Sv(a){Fq.call(this,1,arguments);this.csn=a}
w(Sv,Fq);var Oq=new Gq("screen-created",Sv),Tv=[],Uv=0,Vv=new Map,Wv=new Map,Xv=new Map;
function Yv(a,b,c,d,e){e=e===void 0?!1:e;for(var f=Zv({cttAuthInfo:Qu(b)||void 0},b),g=y(d),h=g.next();!h.done;h=g.next()){h=h.value;var k=h.getAsJson();(wg(k)||!k.trackingParams&&!k.veType)&&V(Error("Child VE logged with no data"));if(R("no_client_ve_attach_unless_shown")){var l=$v(h,b);if(k.veType&&!Wv.has(l)&&!Xv.has(l)&&!e){if(!R("il_attach_cache_limit")||Vv.size<1E3){Vv.set(l,[a,b,c,h]);return}R("il_attach_cache_limit")&&Vv.size>1E3&&V(new T("IL Attach cache exceeded limit"))}h=$v(c,b);Vv.has(h)?
aw(c,b):Xv.set(h,!0)}}d=d.filter(function(m){m.csn!==b?(m.csn=b,m=!0):m=!1;return m});
c={csn:b,parentVe:c.getAsJson(),childVes:Ub(d,function(m){return m.getAsJson()})};
b==="UNDEFINED_CSN"?bw("visualElementAttached",f,c):a?ou("visualElementAttached",c,a,f):Jo("visualElementAttached",c,f)}
function bw(a,b,c){Tv.push({Te:a,payload:c,Gh:void 0,options:b});Uv||(Uv=Pq())}
function Qq(a){if(Tv){for(var b=y(Tv),c=b.next();!c.done;c=b.next())c=c.value,c.payload&&(c.payload.csn=a.csn,Jo(c.Te,c.payload,c.options));Tv.length=0}Uv=0}
function $v(a,b){return""+a.getAsJson().veType+a.getAsJson().veCounter+b}
function aw(a,b){a=$v(a,b);Vv.has(a)&&(b=Vv.get(a)||[],Yv(b[0],b[1],b[2],[b[3]],!0),Vv.delete(a))}
function Zv(a,b){R("log_sequence_info_on_gel_web")&&(a.sequenceGroup=b);return a}
;function cw(){try{return!!self.localStorage}catch(a){return!1}}
;function dw(a){a=a.match(/(.*)::.*::.*/);if(a!==null)return a[1]}
function ew(a){if(cw()){var b=Object.keys(window.localStorage);b=y(b);for(var c=b.next();!c.done;c=b.next()){c=c.value;var d=dw(c);d===void 0||a.includes(d)||self.localStorage.removeItem(c)}}}
function fw(){if(!cw())return!1;var a=Sn(),b=Object.keys(window.localStorage);b=y(b);for(var c=b.next();!c.done;c=b.next())if(c=dw(c.value),c!==void 0&&c!==a)return!0;return!1}
;function gw(){var a=!1;try{a=!!window.sessionStorage.getItem("session_logininfo")}catch(b){a=!0}return(P("INNERTUBE_CLIENT_NAME")==="WEB"||P("INNERTUBE_CLIENT_NAME")==="WEB_CREATOR")&&a}
function hw(a){if(P("LOGGED_IN",!0)&&gw()){var b=P("VALID_SESSION_TEMPDATA_DOMAINS",[]);var c=ic(window.location.href);c&&b.push(c);c=ic(a);Rb(b,c)>=0||!c&&a.lastIndexOf("/",0)==0?(b=jc(a),(b=kc(b))?(b=Rv(b),b=(b=Bn(b)||null)?Em(b):{}):b=null):b=null;b==null&&(b={});c=b;var d=void 0;gw()?(d||(d=P("LOGIN_INFO")),d?(c.session_logininfo=d,c=!0):c=!1):c=!1;c&&Pv(a,b)}}
;function iw(a,b,c){b=b===void 0?{}:b;c=c===void 0?!1:c;var d=P("EVENT_ID");d&&(b.ei||(b.ei=d));b&&Pv(a,b);if(c)return!1;hw(a);var e=e===void 0?{}:e;var f=f===void 0?"":f;var g=g===void 0?window:g;b=nc(a,e);hw(b);a=void 0;a=a===void 0?zb:a;a:if(f=b+f,a=a===void 0?zb:a,!(f instanceof vb)){for(b=0;b<a.length;++b)if(c=a[b],c instanceof xb&&c.He(f)){f=new vb(f);break a}f=void 0}g=g.location;f=Bb(f||wb);f!==void 0&&(g.href=f);return!0}
;function jw(a){if(yg(P("PLAYER_VARS",{}))!="1"){a&&im();try{jv().then(function(){},function(){}),Om(jw,18E5)}catch(b){wm(b)}}}
;var kw=new Map([["dark","USER_INTERFACE_THEME_DARK"],["light","USER_INTERFACE_THEME_LIGHT"]]);function lw(){var a=a===void 0?window.location.href:a;if(R("kevlar_disable_theme_param"))return null;var b=fc(hc(5,a));if(R("enable_dark_theme_only_on_shorts")&&b!=null&&b.startsWith("/shorts/"))return"USER_INTERFACE_THEME_DARK";try{var c=Fm(a).theme;return kw.get(c)||null}catch(d){}return null}
;function mw(){this.h={};if(this.i=Dn()){var a=Bn("CONSISTENCY");a&&nw(this,{encryptedTokenJarContents:a})}}
mw.prototype.handleResponse=function(a,b){if(!b)throw Error("request needs to be passed into ConsistencyService");var c,d;b=((c=b.Ga.context)==null?void 0:(d=c.request)==null?void 0:d.consistencyTokenJars)||[];var e;if(a=(e=a.responseContext)==null?void 0:e.consistencyTokenJar){e=y(b);for(c=e.next();!c.done;c=e.next())delete this.h[c.value.encryptedTokenJarContents];nw(this,a)}};
function nw(a,b){if(b.encryptedTokenJarContents&&(a.h[b.encryptedTokenJarContents]=b,typeof b.expirationSeconds==="string")){var c=Number(b.expirationSeconds);setTimeout(function(){delete a.h[b.encryptedTokenJarContents]},c*1E3);
a.i&&An("CONSISTENCY",b.encryptedTokenJarContents,c,void 0,!0)}}
;var ow=window.location.hostname.split(".").slice(-2).join(".");function pw(){this.j=-1;var a=P("LOCATION_PLAYABILITY_TOKEN");P("INNERTUBE_CLIENT_NAME")==="TVHTML5"&&(this.h=qw(this))&&(a=this.h.get("yt-location-playability-token"));a&&(this.locationPlayabilityToken=a,this.i=void 0)}
var rw;function sw(){rw=E("yt.clientLocationService.instance");rw||(rw=new pw,D("yt.clientLocationService.instance",rw));return rw}
r=pw.prototype;
r.setLocationOnInnerTubeContext=function(a){a.client||(a.client={});if(this.i)a.client.locationInfo||(a.client.locationInfo={}),a.client.locationInfo.latitudeE7=Math.floor(this.i.coords.latitude*1E7),a.client.locationInfo.longitudeE7=Math.floor(this.i.coords.longitude*1E7),a.client.locationInfo.horizontalAccuracyMeters=Math.round(this.i.coords.accuracy),a.client.locationInfo.forceLocationPlayabilityTokenRefresh=!0;else if(this.o||this.locationPlayabilityToken)a.client.locationPlayabilityToken=this.o||
this.locationPlayabilityToken};
r.handleResponse=function(a){var b;a=(b=a.responseContext)==null?void 0:b.locationPlayabilityToken;a!==void 0&&(this.locationPlayabilityToken=a,this.i=void 0,P("INNERTUBE_CLIENT_NAME")==="TVHTML5"?(this.h=qw(this))&&this.h.set("yt-location-playability-token",a,15552E3):An("YT_CL",JSON.stringify({loctok:a}),15552E3,ow,!0))};
function qw(a){return a.h===void 0?new Ao("yt-client-location"):a.h}
r.clearLocationPlayabilityToken=function(a){a==="TVHTML5"?(this.h=qw(this))&&this.h.remove("yt-location-playability-token"):Cn("YT_CL");this.o=void 0;this.j!==-1&&(clearTimeout(this.j),this.j=-1)};
r.getCurrentPositionFromGeolocation=function(){var a=this;if(!(navigator&&navigator.geolocation&&navigator.geolocation.getCurrentPosition))return Promise.reject(Error("Geolocation unsupported"));var b=!1,c=1E4;P("INNERTUBE_CLIENT_NAME")==="MWEB"&&(b=!0,c=15E3);return new Promise(function(d,e){navigator.geolocation.getCurrentPosition(function(f){a.i=f;d(f)},function(f){e(f)},{enableHighAccuracy:b,
maximumAge:0,timeout:c})})};
r.createUnpluggedLocationInfo=function(a){var b={};a=a.coords;if(a==null?0:a.latitude)b.latitudeE7=Math.floor(a.latitude*1E7);if(a==null?0:a.longitude)b.longitudeE7=Math.floor(a.longitude*1E7);if(a==null?0:a.accuracy)b.locationRadiusMeters=Math.round(a.accuracy);return b};
r.createLocationInfo=function(a){var b={};a=a.coords;if(a==null?0:a.latitude)b.latitudeE7=Math.floor(a.latitude*1E7);if(a==null?0:a.longitude)b.longitudeE7=Math.floor(a.longitude*1E7);return b};function tw(a,b,c){b=b===void 0?!1:b;c=c===void 0?!1:c;var d=P("INNERTUBE_CONTEXT");if(!d)return wu(Error("Error: No InnerTubeContext shell provided in ytconfig.")),{};d=Bg(d);R("web_no_tracking_params_in_shell_killswitch")||delete d.clickTracking;d.client||(d.client={});var e=d.client;e.clientName==="MWEB"&&e.clientFormFactor!=="AUTOMOTIVE_FORM_FACTOR"&&(e.clientFormFactor=P("IS_TABLET")?"LARGE_FORM_FACTOR":"SMALL_FORM_FACTOR");e.screenWidthPoints=window.innerWidth;e.screenHeightPoints=window.innerHeight;
e.screenPixelDensity=Math.round(window.devicePixelRatio||1);e.screenDensityFloat=window.devicePixelRatio||1;e.utcOffsetMinutes=-Math.floor((new Date).getTimezoneOffset());var f=f===void 0?!1:f;Hn();var g="USER_INTERFACE_THEME_LIGHT";Kn(165)?g="USER_INTERFACE_THEME_DARK":Kn(174)?g="USER_INTERFACE_THEME_LIGHT":!R("kevlar_legacy_browsers")&&window.matchMedia&&window.matchMedia("(prefers-color-scheme)").matches&&window.matchMedia("(prefers-color-scheme: dark)").matches&&(g="USER_INTERFACE_THEME_DARK");
f=f?g:lw()||g;e.userInterfaceTheme=f;if(!b){if(f=Pn())e.connectionType=f;R("web_log_effective_connection_type")&&(f=Qn())&&(d.client.effectiveConnectionType=f)}var h;if(R("web_log_memory_total_kbytes")&&((h=C.navigator)==null?0:h.deviceMemory)){var k;h=(k=C.navigator)==null?void 0:k.deviceMemory;d.client.memoryTotalKbytes=""+h*1E6}R("web_gcf_hashes_innertube")&&(f=wq())&&(k=f.coldConfigData,h=f.coldHashData,f=f.hotHashData,d.client.configInfo=d.client.configInfo||{},k&&(d.client.configInfo.coldConfigData=
k),h&&(d.client.configInfo.coldHashData=h),f&&(d.client.configInfo.hotHashData=f));k=Fm(C.location.href);!R("web_populate_internal_geo_killswitch")&&k.internalcountrycode&&(e.internalGeo=k.internalcountrycode);e.clientName==="MWEB"||e.clientName==="WEB"?(e.mainAppWebInfo={graftUrl:C.location.href},R("kevlar_woffle")&&un.h&&(k=un.h,e.mainAppWebInfo.pwaInstallabilityStatus=!k.h&&k.i?"PWA_INSTALLABILITY_STATUS_CAN_BE_INSTALLED":"PWA_INSTALLABILITY_STATUS_UNKNOWN"),e.mainAppWebInfo.webDisplayMode=vn(),
e.mainAppWebInfo.isWebNativeShareAvailable=navigator&&navigator.share!==void 0):e.clientName==="TVHTML5"&&(!R("web_lr_app_quality_killswitch")&&(k=P("LIVING_ROOM_APP_QUALITY"))&&(e.tvAppInfo=Object.assign(e.tvAppInfo||{},{appQuality:k})),k=P("LIVING_ROOM_CERTIFICATION_SCOPE"))&&(e.tvAppInfo=Object.assign(e.tvAppInfo||{},{certificationScope:k}));if(!R("web_populate_time_zone_itc_killswitch")){a:{if(typeof Intl!=="undefined")try{var l=(new Intl.DateTimeFormat).resolvedOptions().timeZone;break a}catch(Oa){}l=
void 0}l&&(e.timeZone=l)}(l=P("EXPERIMENTS_TOKEN",""))?e.experimentsToken=l:delete e.experimentsToken;l=Rm();mw.h||(mw.h=new mw);e=mw.h.h;k=[];h=0;for(var m in e)k[h++]=e[m];d.request=Object.assign({},d.request,{internalExperimentFlags:l,consistencyTokenJars:k});!R("web_prequest_context_killswitch")&&(m=P("INNERTUBE_CONTEXT_PREQUEST_CONTEXT"))&&(d.request.externalPrequestContext=m);l=Hn();m=Kn(58);l=l.get("gsml","");d.user=Object.assign({},d.user);m&&(d.user.enableSafetyMode=m);l&&(d.user.lockedSafetyMode=
!0);R("warm_op_csn_cleanup")?c&&(b=Ou())&&(d.clientScreenNonce=b):!b&&(b=Ou())&&(d.clientScreenNonce=b);a&&(d.clickTracking={clickTrackingParams:a});if(a=E("yt.mdx.remote.remoteClient_"))d.remoteClient=a;sw().setLocationOnInnerTubeContext(d);try{var n=Jm(),p=n.bid;delete n.bid;d.adSignalsInfo={params:[],bid:p};for(var t=y(Object.entries(n)),u=t.next();!u.done;u=t.next()){var x=y(u.value),z=x.next().value,H=x.next().value;n=z;p=H;a=void 0;(a=d.adSignalsInfo.params)==null||a.push({key:n,value:""+p})}var K,
da;if(((K=d.client)==null?void 0:K.clientName)==="TVHTML5"||((da=d.client)==null?void 0:da.clientName)==="TVHTML5_UNPLUGGED"){var ea=P("INNERTUBE_CONTEXT");ea.adSignalsInfo&&(d.adSignalsInfo.advertisingId=ea.adSignalsInfo.advertisingId,d.adSignalsInfo.advertisingIdSignalType="DEVICE_ID_TYPE_CONNECTED_TV_IFA",d.adSignalsInfo.limitAdTracking=ea.adSignalsInfo.limitAdTracking)}}catch(Oa){wu(Oa)}return d}
;function uw(a){var b={"Content-Type":"application/json"};P("EOM_VISITOR_DATA")?b["X-Goog-EOM-Visitor-Id"]=P("EOM_VISITOR_DATA"):P("VISITOR_DATA")&&(b["X-Goog-Visitor-Id"]=P("VISITOR_DATA"));b["X-Youtube-Bootstrap-Logged-In"]=P("LOGGED_IN",!1);P("DEBUG_SETTINGS_METADATA")&&(b["X-Debug-Settings-Metadata"]=P("DEBUG_SETTINGS_METADATA"));a!=="cors"&&((a=P("INNERTUBE_CONTEXT_CLIENT_NAME"))&&(b["X-Youtube-Client-Name"]=a),(a=P("INNERTUBE_CONTEXT_CLIENT_VERSION"))&&(b["X-Youtube-Client-Version"]=a),(a=P("CHROME_CONNECTED_HEADER"))&&
(b["X-Youtube-Chrome-Connected"]=a),(a=P("DOMAIN_ADMIN_STATE"))&&(b["X-Youtube-Domain-Admin-State"]=a),P("ENABLE_LAVA_HEADER_ON_IT_EXPANSION")&&(a=P("SERIALIZED_LAVA_DEVICE_CONTEXT"))&&(b["X-YouTube-Lava-Device-Context"]=a));return b}
;function vw(){this.h={}}
vw.prototype.contains=function(a){return Object.prototype.hasOwnProperty.call(this.h,a)};
vw.prototype.get=function(a){if(this.contains(a))return this.h[a]};
vw.prototype.set=function(a,b){this.h[a]=b};
vw.prototype.remove=function(a){delete this.h[a]};function ww(){this.mappings=new vw}
ww.prototype.getModuleId=function(a){return a.serviceId.getModuleId()};
ww.prototype.get=function(a){a:{var b=this.mappings.get(a.toString());switch(b.type){case "mapping":a=b.value;break a;case "factory":b=b.value();this.mappings.set(a.toString(),{type:"mapping",value:b});a=b;break a;default:a=Db(b)}}return a};
new ww;function xw(a){return function(){return new a}}
;var yw={},zw=(yw.WEB_UNPLUGGED="^unplugged/",yw.WEB_UNPLUGGED_ONBOARDING="^unplugged/",yw.WEB_UNPLUGGED_OPS="^unplugged/",yw.WEB_UNPLUGGED_PUBLIC="^unplugged/",yw.WEB_CREATOR="^creator/",yw.WEB_KIDS="^kids/",yw.WEB_EXPERIMENTS="^experiments/",yw.WEB_MUSIC="^music/",yw.WEB_REMIX="^music/",yw.WEB_MUSIC_EMBEDDED_PLAYER="^music/",yw.WEB_MUSIC_EMBEDDED_PLAYER="^main_app/|^sfv/",yw);
function Aw(a){var b=b===void 0?"UNKNOWN_INTERFACE":b;if(a.length===1)return a[0];var c=zw[b];if(c){c=new RegExp(c);for(var d=y(a),e=d.next();!e.done;e=d.next())if(e=e.value,c.exec(e))return e}var f=[];Object.entries(zw).forEach(function(g){var h=y(g);g=h.next().value;h=h.next().value;b!==g&&f.push(h)});
c=new RegExp(f.join("|"));a.sort(function(g,h){return g.length-h.length});
d=y(a);for(e=d.next();!e.done;e=d.next())if(e=e.value,!c.exec(e))return e;return a[0]}
;function Bw(){}
Bw.prototype.u=function(a,b,c){b=b===void 0?{}:b;c=c===void 0?zn:c;var d={context:tw(a.clickTrackingParams,!1,this.o)};var e=this.i(a);if(e){this.h(d,e,b);var f;b="/youtubei/v1/"+Aw(this.j());(e=(f=Gt(a.commandMetadata,Xl))==null?void 0:f.apiUrl)&&(b=e);f=Zu(Yu(b));a=Object.assign({},{command:a},void 0);d={input:f,Za:$u(f),Ga:d,config:a};d.config.Mb?d.config.Mb.identity=c:d.config.Mb={identity:c};return d}wu(new T("Error: Failed to create Request from Command.",a))};
fa.Object.defineProperties(Bw.prototype,{o:{configurable:!0,enumerable:!0,get:function(){return!1}}});
function Cw(){}
w(Cw,Bw);function Dw(){}
w(Dw,Cw);Dw.prototype.u=function(){return{input:"/getDatasyncIdsEndpoint",Za:$u("/getDatasyncIdsEndpoint","GET"),Ga:{}}};
Dw.prototype.j=function(){return[]};
Dw.prototype.i=function(){};
Dw.prototype.h=function(){};var Ew={},Fw=(Ew.GET_DATASYNC_IDS=xw(Dw),Ew);function Gw(a){var b;(b=E("ytcsi."+(a||"")+"data_"))||(b={tick:{},info:{}},D("ytcsi."+(a||"")+"data_",b));return b}
function Hw(){var a=Gw();a.info||(a.info={});return a.info}
function Iw(a){a=Gw(a);a.metadata||(a.metadata={});return a.metadata}
function Jw(a){a=Gw(a);a.tick||(a.tick={});return a.tick}
function Kw(a){a=Gw(a);if(a.gel){var b=a.gel;b.gelInfos||(b.gelInfos={});b.gelTicks||(b.gelTicks={})}else a.gel={gelTicks:{},gelInfos:{}};return a.gel}
function Lw(a){a=Kw(a);a.gelInfos||(a.gelInfos={});return a.gelInfos}
function Mw(a){var b=Gw(a).nonce;b||(b=Eu(),Gw(a).nonce=b);return b}
;function Nw(){var a=E("ytcsi.debug");a||(a=[],D("ytcsi.debug",a),D("ytcsi.reference",{}));return a}
function Ow(a){a=a||"";var b=E("ytcsi.reference");b||(Nw(),b=E("ytcsi.reference"));if(b[a])return b[a];var c=Nw(),d={timerName:a,info:{},tick:{},span:{},jspbInfo:[]};c.push(d);return b[a]=d}
;var W={},Pw=(W["analytics.explore"]="LATENCY_ACTION_CREATOR_ANALYTICS_EXPLORE",W["artist.analytics"]="LATENCY_ACTION_CREATOR_ARTIST_ANALYTICS",W["artist.events"]="LATENCY_ACTION_CREATOR_ARTIST_CONCERTS",W["artist.presskit"]="LATENCY_ACTION_CREATOR_ARTIST_PROFILE",W["asset.claimed_videos"]="LATENCY_ACTION_CREATOR_CMS_ASSET_CLAIMED_VIDEOS",W["asset.composition"]="LATENCY_ACTION_CREATOR_CMS_ASSET_COMPOSITION",W["asset.composition_ownership"]="LATENCY_ACTION_CREATOR_CMS_ASSET_COMPOSITION_OWNERSHIP",W["asset.composition_policy"]=
"LATENCY_ACTION_CREATOR_CMS_ASSET_COMPOSITION_POLICY",W["asset.embeds"]="LATENCY_ACTION_CREATOR_CMS_ASSET_EMBEDS",W["asset.history"]="LATENCY_ACTION_CREATOR_CMS_ASSET_HISTORY",W["asset.issues"]="LATENCY_ACTION_CREATOR_CMS_ASSET_ISSUES",W["asset.licenses"]="LATENCY_ACTION_CREATOR_CMS_ASSET_LICENSES",W["asset.metadata"]="LATENCY_ACTION_CREATOR_CMS_ASSET_METADATA",W["asset.ownership"]="LATENCY_ACTION_CREATOR_CMS_ASSET_OWNERSHIP",W["asset.policy"]="LATENCY_ACTION_CREATOR_CMS_ASSET_POLICY",W["asset.references"]=
"LATENCY_ACTION_CREATOR_CMS_ASSET_REFERENCES",W["asset.shares"]="LATENCY_ACTION_CREATOR_CMS_ASSET_SHARES",W["asset.sound_recordings"]="LATENCY_ACTION_CREATOR_CMS_ASSET_SOUND_RECORDINGS",W["asset_group.assets"]="LATENCY_ACTION_CREATOR_CMS_ASSET_GROUP_ASSETS",W["asset_group.campaigns"]="LATENCY_ACTION_CREATOR_CMS_ASSET_GROUP_CAMPAIGNS",W["asset_group.claimed_videos"]="LATENCY_ACTION_CREATOR_CMS_ASSET_GROUP_CLAIMED_VIDEOS",W["asset_group.metadata"]="LATENCY_ACTION_CREATOR_CMS_ASSET_GROUP_METADATA",W["song.analytics"]=
"LATENCY_ACTION_CREATOR_SONG_ANALYTICS",W.creator_channel_dashboard="LATENCY_ACTION_CREATOR_CHANNEL_DASHBOARD",W["channel.analytics"]="LATENCY_ACTION_CREATOR_CHANNEL_ANALYTICS",W["channel.comments"]="LATENCY_ACTION_CREATOR_CHANNEL_COMMENTS",W["channel.content"]="LATENCY_ACTION_CREATOR_POST_LIST",W["channel.content.promotions"]="LATENCY_ACTION_CREATOR_PROMOTION_LIST",W["channel.copyright"]="LATENCY_ACTION_CREATOR_CHANNEL_COPYRIGHT",W["channel.editing"]="LATENCY_ACTION_CREATOR_CHANNEL_EDITING",W["channel.monetization"]=
"LATENCY_ACTION_CREATOR_CHANNEL_MONETIZATION",W["channel.music"]="LATENCY_ACTION_CREATOR_CHANNEL_MUSIC",W["channel.music_storefront"]="LATENCY_ACTION_CREATOR_CHANNEL_MUSIC_STOREFRONT",W["channel.playlists"]="LATENCY_ACTION_CREATOR_CHANNEL_PLAYLISTS",W["channel.translations"]="LATENCY_ACTION_CREATOR_CHANNEL_TRANSLATIONS",W["channel.videos"]="LATENCY_ACTION_CREATOR_CHANNEL_VIDEOS",W["channel.live_streaming"]="LATENCY_ACTION_CREATOR_LIVE_STREAMING",W["dialog.copyright_strikes"]="LATENCY_ACTION_CREATOR_DIALOG_COPYRIGHT_STRIKES",
W["dialog.video_copyright"]="LATENCY_ACTION_CREATOR_DIALOG_VIDEO_COPYRIGHT",W["dialog.uploads"]="LATENCY_ACTION_CREATOR_DIALOG_UPLOADS",W.owner="LATENCY_ACTION_CREATOR_CMS_DASHBOARD",W["owner.allowlist"]="LATENCY_ACTION_CREATOR_CMS_ALLOWLIST",W["owner.analytics"]="LATENCY_ACTION_CREATOR_CMS_ANALYTICS",W["owner.art_tracks"]="LATENCY_ACTION_CREATOR_CMS_ART_TRACKS",W["owner.assets"]="LATENCY_ACTION_CREATOR_CMS_ASSETS",W["owner.asset_groups"]="LATENCY_ACTION_CREATOR_CMS_ASSET_GROUPS",W["owner.bulk"]=
"LATENCY_ACTION_CREATOR_CMS_BULK_HISTORY",W["owner.campaigns"]="LATENCY_ACTION_CREATOR_CMS_CAMPAIGNS",W["owner.channel_invites"]="LATENCY_ACTION_CREATOR_CMS_CHANNEL_INVITES",W["owner.channels"]="LATENCY_ACTION_CREATOR_CMS_CHANNELS",W["owner.claimed_videos"]="LATENCY_ACTION_CREATOR_CMS_CLAIMED_VIDEOS",W["owner.claims"]="LATENCY_ACTION_CREATOR_CMS_MANUAL_CLAIMING",W["owner.claims.manual"]="LATENCY_ACTION_CREATOR_CMS_MANUAL_CLAIMING",W["owner.delivery"]="LATENCY_ACTION_CREATOR_CMS_CONTENT_DELIVERY",
W["owner.delivery_templates"]="LATENCY_ACTION_CREATOR_CMS_DELIVERY_TEMPLATES",W["owner.issues"]="LATENCY_ACTION_CREATOR_CMS_ISSUES",W["owner.licenses"]="LATENCY_ACTION_CREATOR_CMS_LICENSES",W["owner.pitch_music"]="LATENCY_ACTION_CREATOR_CMS_PITCH_MUSIC",W["owner.policies"]="LATENCY_ACTION_CREATOR_CMS_POLICIES",W["owner.releases"]="LATENCY_ACTION_CREATOR_CMS_RELEASES",W["owner.reports"]="LATENCY_ACTION_CREATOR_CMS_REPORTS",W["owner.videos"]="LATENCY_ACTION_CREATOR_CMS_VIDEOS",W["playlist.videos"]=
"LATENCY_ACTION_CREATOR_PLAYLIST_VIDEO_LIST",W["post.comments"]="LATENCY_ACTION_CREATOR_POST_COMMENTS",W["post.edit"]="LATENCY_ACTION_CREATOR_POST_EDIT",W["promotion.edit"]="LATENCY_ACTION_CREATOR_PROMOTION_EDIT",W["video.analytics"]="LATENCY_ACTION_CREATOR_VIDEO_ANALYTICS",W["video.claims"]="LATENCY_ACTION_CREATOR_VIDEO_CLAIMS",W["video.comments"]="LATENCY_ACTION_CREATOR_VIDEO_COMMENTS",W["video.copyright"]="LATENCY_ACTION_CREATOR_VIDEO_COPYRIGHT",W["video.edit"]="LATENCY_ACTION_CREATOR_VIDEO_EDIT",
W["video.editor"]="LATENCY_ACTION_CREATOR_VIDEO_VIDEO_EDITOR",W["video.editor_async"]="LATENCY_ACTION_CREATOR_VIDEO_VIDEO_EDITOR_ASYNC",W["video.live_settings"]="LATENCY_ACTION_CREATOR_VIDEO_LIVE_SETTINGS",W["video.live_streaming"]="LATENCY_ACTION_CREATOR_VIDEO_LIVE_STREAMING",W["video.monetization"]="LATENCY_ACTION_CREATOR_VIDEO_MONETIZATION",W["video.policy"]="LATENCY_ACTION_CREATOR_VIDEO_POLICY",W["video.rights_management"]="LATENCY_ACTION_CREATOR_VIDEO_RIGHTS_MANAGEMENT",W["video.translations"]=
"LATENCY_ACTION_CREATOR_VIDEO_TRANSLATIONS",W),X={},Qw=(X.auto_search="LATENCY_ACTION_AUTO_SEARCH",X.ad_to_ad="LATENCY_ACTION_AD_TO_AD",X.ad_to_video="LATENCY_ACTION_AD_TO_VIDEO",X.app_startup="LATENCY_ACTION_APP_STARTUP",X.browse="LATENCY_ACTION_BROWSE",X.cast_splash="LATENCY_ACTION_CAST_SPLASH",X.channel_activity="LATENCY_ACTION_KIDS_CHANNEL_ACTIVITY",X.channels="LATENCY_ACTION_CHANNELS",X.chips="LATENCY_ACTION_CHIPS",X.commerce_transaction="LATENCY_ACTION_COMMERCE_TRANSACTION",X.direct_playback=
"LATENCY_ACTION_DIRECT_PLAYBACK",X.editor="LATENCY_ACTION_EDITOR",X.embed="LATENCY_ACTION_EMBED",X.entity_key_serialization_perf="LATENCY_ACTION_ENTITY_KEY_SERIALIZATION_PERF",X.entity_key_deserialization_perf="LATENCY_ACTION_ENTITY_KEY_DESERIALIZATION_PERF",X.explore="LATENCY_ACTION_EXPLORE",X.favorites="LATENCY_ACTION_FAVORITES",X.home="LATENCY_ACTION_HOME",X.inboarding="LATENCY_ACTION_INBOARDING",X.library="LATENCY_ACTION_LIBRARY",X.live="LATENCY_ACTION_LIVE",X.live_pagination="LATENCY_ACTION_LIVE_PAGINATION",
X.management="LATENCY_ACTION_MANAGEMENT",X.mini_app="LATENCY_ACTION_MINI_APP_PLAY",X.notification_settings="LATENCY_ACTION_KIDS_NOTIFICATION_SETTINGS",X.onboarding="LATENCY_ACTION_ONBOARDING",X.parent_profile_settings="LATENCY_ACTION_KIDS_PARENT_PROFILE_SETTINGS",X.parent_tools_collection="LATENCY_ACTION_PARENT_TOOLS_COLLECTION",X.parent_tools_dashboard="LATENCY_ACTION_PARENT_TOOLS_DASHBOARD",X.player_att="LATENCY_ACTION_PLAYER_ATTESTATION",X.prebuffer="LATENCY_ACTION_PREBUFFER",X.prefetch="LATENCY_ACTION_PREFETCH",
X.profile_settings="LATENCY_ACTION_KIDS_PROFILE_SETTINGS",X.profile_switcher="LATENCY_ACTION_LOGIN",X.projects="LATENCY_ACTION_PROJECTS",X.reel_watch="LATENCY_ACTION_REEL_WATCH",X.results="LATENCY_ACTION_RESULTS",X.red="LATENCY_ACTION_PREMIUM_PAGE_GET_BROWSE",X.premium="LATENCY_ACTION_PREMIUM_PAGE_GET_BROWSE",X.privacy_policy="LATENCY_ACTION_KIDS_PRIVACY_POLICY",X.review="LATENCY_ACTION_REVIEW",X.search_overview_answer="LATENCY_ACTION_SEARCH_OVERVIEW_ANSWER",X.search_ui="LATENCY_ACTION_SEARCH_UI",
X.search_suggest="LATENCY_ACTION_SUGGEST",X.search_zero_state="LATENCY_ACTION_SEARCH_ZERO_STATE",X.secret_code="LATENCY_ACTION_KIDS_SECRET_CODE",X.seek="LATENCY_ACTION_PLAYER_SEEK",X.settings="LATENCY_ACTION_SETTINGS",X.store="LATENCY_ACTION_STORE",X.supervision_dashboard="LATENCY_ACTION_KIDS_SUPERVISION_DASHBOARD",X.tenx="LATENCY_ACTION_TENX",X.video_to_ad="LATENCY_ACTION_VIDEO_TO_AD",X.watch="LATENCY_ACTION_WATCH",X.watch_it_again="LATENCY_ACTION_KIDS_WATCH_IT_AGAIN",X["watch,watch7"]="LATENCY_ACTION_WATCH",
X["watch,watch7_html5"]="LATENCY_ACTION_WATCH",X["watch,watch7ad"]="LATENCY_ACTION_WATCH",X["watch,watch7ad_html5"]="LATENCY_ACTION_WATCH",X.wn_comments="LATENCY_ACTION_LOAD_COMMENTS",X.ww_rqs="LATENCY_ACTION_WHO_IS_WATCHING",X.voice_assistant="LATENCY_ACTION_VOICE_ASSISTANT",X.cast_load_by_entity_to_watch="LATENCY_ACTION_CAST_LOAD_BY_ENTITY_TO_WATCH",X.networkless_performance="LATENCY_ACTION_NETWORKLESS_PERFORMANCE",X.gel_compression="LATENCY_ACTION_GEL_COMPRESSION",X.gel_jspb_serialize="LATENCY_ACTION_GEL_JSPB_SERIALIZE",
X.attestation_challenge_fetch="LATENCY_ACTION_ATTESTATION_CHALLENGE_FETCH",X);Object.assign(Qw,Pw);function Rw(a,b){Fq.call(this,1,arguments);this.timer=b}
w(Rw,Fq);var Sw=new Gq("aft-recorded",Rw);D("ytLoggingGelSequenceIdObj_",C.ytLoggingGelSequenceIdObj_||{});var Tw=C.ytLoggingLatencyUsageStats_||{};D("ytLoggingLatencyUsageStats_",Tw);function Uw(){this.h=0}
function Vw(){Uw.h||(Uw.h=new Uw);return Uw.h}
Uw.prototype.tick=function(a,b,c,d){Ww(this,"tick_"+a+"_"+b)||Jo("latencyActionTicked",{tickName:a,clientActionNonce:b},{timestamp:c,cttAuthInfo:d})};
Uw.prototype.info=function(a,b,c){var d=Object.keys(a).join("");Ww(this,"info_"+d+"_"+b)||(a=Object.assign({},a),a.clientActionNonce=b,Jo("latencyActionInfo",a,{cttAuthInfo:c}))};
Uw.prototype.jspbInfo=function(){};
Uw.prototype.span=function(a,b,c){var d=Object.keys(a).join("");Ww(this,"span_"+d+"_"+b)||(a.clientActionNonce=b,Jo("latencyActionSpan",a,{cttAuthInfo:c}))};
function Ww(a,b){Tw[b]=Tw[b]||{count:0};var c=Tw[b];c.count++;c.time=U();a.h||(a.h=Un(function(){var d=U(),e;for(e in Tw)Tw[e]&&d-Tw[e].time>6E4&&delete Tw[e];a&&(a.h=0)},5E3));
return c.count>5?(c.count===6&&Math.random()*1E5<1&&(c=new T("CSI data exceeded logging limit with key",b.split("_")),b.indexOf("plev")>=0||V(c)),!0):!1}
;var Xw=window;function Yw(){this.timing={};this.clearResourceTimings=function(){};
this.webkitClearResourceTimings=function(){};
this.mozClearResourceTimings=function(){};
this.msClearResourceTimings=function(){};
this.oClearResourceTimings=function(){}}
function Zw(){var a;if(R("csi_use_performance_navigation_timing")||R("csi_use_performance_navigation_timing_tvhtml5")){var b,c,d,e=Y==null?void 0:(a=Y.getEntriesByType)==null?void 0:(b=a.call(Y,"navigation"))==null?void 0:(c=b[0])==null?void 0:(d=c.toJSON)==null?void 0:d.call(c);e?(e.requestStart=$w(e.requestStart),e.responseEnd=$w(e.responseEnd),e.redirectStart=$w(e.redirectStart),e.redirectEnd=$w(e.redirectEnd),e.domainLookupEnd=$w(e.domainLookupEnd),e.connectStart=$w(e.connectStart),e.connectEnd=
$w(e.connectEnd),e.responseStart=$w(e.responseStart),e.secureConnectionStart=$w(e.secureConnectionStart),e.domainLookupStart=$w(e.domainLookupStart),e.isPerformanceNavigationTiming=!0,a=e):a=Y.timing}else a=R("csi_performance_timing_to_object")?JSON.parse(JSON.stringify(Y.timing)):Y.timing;return a}
function $w(a){return Math.round(ax()+a)}
function ax(){return(R("csi_use_time_origin")||R("csi_use_time_origin_tvhtml5"))&&Y.timeOrigin?Math.floor(Y.timeOrigin):Y.timing.navigationStart}
var Y=Xw.performance||Xw.mozPerformance||Xw.msPerformance||Xw.webkitPerformance||new Yw;var bx=!1,cx=!1,dx={'script[name="scheduler/scheduler"]':"sj",'script[name="player/base"]':"pj",'link[rel="preload"][name="player/embed"]':"pej",'link[rel="stylesheet"][name="www-player"]':"pc",'link[rel="stylesheet"][name="player/www-player"]':"pc",'script[name="desktop_polymer/desktop_polymer"]':"dpj",'link[rel="import"][name="desktop_polymer"]':"dph",'script[name="mobile-c3"]':"mcj",'link[rel="stylesheet"][name="mobile-c3"]':"mcc",'script[name="player-plasma-ias-phone/base"]':"mcppj",'script[name="player-plasma-ias-tablet/base"]':"mcptj",
'link[rel="stylesheet"][name="mobile-polymer-player-ias"]':"mcpc",'link[rel="stylesheet"][name="mobile-polymer-player-svg-ias"]':"mcpsc",'script[name="mobile_blazer_core_mod"]':"mbcj",'link[rel="stylesheet"][name="mobile_blazer_css"]':"mbc",'script[name="mobile_blazer_logged_in_users_mod"]':"mbliuj",'script[name="mobile_blazer_logged_out_users_mod"]':"mblouj",'script[name="mobile_blazer_noncore_mod"]':"mbnj","#player_css":"mbpc",'script[name="mobile_blazer_desktopplayer_mod"]':"mbpj",'link[rel="stylesheet"][name="mobile_blazer_tablet_css"]':"mbtc",
'script[name="mobile_blazer_watch_mod"]':"mbwj",'script[name="embed_client"]':"ecj",'link[rel="stylesheet"][name="embed-ui"]':"ecc"};Za(Y.clearResourceTimings||Y.webkitClearResourceTimings||Y.mozClearResourceTimings||Y.msClearResourceTimings||Y.oClearResourceTimings||si,Y);function ex(a,b){if(!R("web_csi_action_sampling_enabled")||!Gw(b).actionDisabled){var c=Ow(b||"");Jv(c.info,a);a.loadType&&(c=a.loadType,Iw(b).loadType=c);Jv(Lw(b),a);c=Mw(b);b=Gw(b).cttAuthInfo;Vw().info(a,c,b)}}
function fx(){var a,b,c,d;return((d=ft().resolve(new $s(sq))==null?void 0:(a=tq())==null?void 0:(b=a.loggingHotConfig)==null?void 0:(c=b.csiConfig)==null?void 0:c.debugTicks)!=null?d:[]).map(function(e){return Object.values(e)[0]})}
function Z(a,b,c){if(!R("web_csi_action_sampling_enabled")||!Gw(c).actionDisabled){var d=Mw(c),e;if(e=R("web_csi_debug_sample_enabled")&&d){(ft().resolve(new $s(sq))==null?0:tq())&&!cx&&(cx=!0,Z("gcfl",U(),c));var f,g,h;e=(ft().resolve(new $s(sq))==null?void 0:(f=tq())==null?void 0:(g=f.loggingHotConfig)==null?void 0:(h=g.csiConfig)==null?void 0:h.debugSampleWeight)||0;if(f=e!==0)b:{f=fx();if(f.length>0)for(g=0;g<f.length;g++)if(a===f[g]){f=!0;break b}f=!1}if(f){for(g=f=0;g<d.length;g++)f=f*31+d.charCodeAt(g),
g<d.length-1&&(f%=0x800000000000);e=f%1E5%e!==0;Gw(c).debugTicksExcludedLogged||(f={},f.debugTicksExcluded=e,ex(f,c));Gw(c).debugTicksExcludedLogged=!0}else e=!1}if(!e){if(a[0]!=="_"&&(e=a,f=b,Y.mark))if(e.startsWith("mark_")||(e="mark_"+e),c&&(e+=" ("+c+")"),f===void 0||R("web_csi_disable_alt_time_performance_mark"))Y.mark(e);else{f=R("csi_use_performance_navigation_timing")||R("csi_use_performance_navigation_timing_tvhtml5")?f-Y.timeOrigin:f-(Y.timeOrigin||Y.timing.navigationStart);try{Y.mark(e,
{startTime:f})}catch(k){}}e=Ow(c||"");e.tick[a]=b||U();if(e.callback&&e.callback[a])for(e=y(e.callback[a]),f=e.next();!f.done;f=e.next())f=f.value,f();e=Kw(c);e.gelTicks&&(e.gelTicks[a]=!0);f=Jw(c);e=b||U();R("log_repeated_ytcsi_ticks")?a in f||(f[a]=e):f[a]=e;f=Gw(c).cttAuthInfo;a==="_start"?(a=Vw(),Ww(a,"baseline_"+d)||Jo("latencyActionBaselined",{clientActionNonce:d},{timestamp:b,cttAuthInfo:f})):Vw().tick(a,d,b,f);gx(c);return e}}}
function hx(){var a=document;if("visibilityState"in a)a=a.visibilityState;else{var b=vs+"VisibilityState";a=b in a?a[b]:void 0}switch(a){case "hidden":return 0;case "visible":return 1;case "prerender":return 2;case "unloaded":return 3;default:return-1}}
function ix(){function a(f,g,h){g=g.match("_rid")?g.split("_rid")[0]:g;typeof h==="number"&&(h=JSON.stringify(h));f.requestIds?f.requestIds.push({endpoint:g,id:h}):f.requestIds=[{endpoint:g,id:h}]}
for(var b={},c=y(Object.entries(P("TIMING_INFO",{}))),d=c.next();!d.done;d=c.next()){var e=y(d.value);d=e.next().value;e=e.next().value;switch(d){case "GetBrowse_rid":a(b,d,e);break;case "GetGuide_rid":a(b,d,e);break;case "GetHome_rid":a(b,d,e);break;case "GetPlayer_rid":a(b,d,e);break;case "GetSearch_rid":a(b,d,e);break;case "GetSettings_rid":a(b,d,e);break;case "GetTrending_rid":a(b,d,e);break;case "GetWatchNext_rid":a(b,d,e);break;case "yt_red":b.isRedSubscriber=!!e;break;case "yt_ad":b.isMonetized=
!!e}}return b}
function jx(a,b){a=document.querySelector(a);if(!a)return!1;var c="",d=a.nodeName;d==="SCRIPT"?(c=a.src,c||(c=a.getAttribute("data-timing-href"))&&(c=window.location.protocol+c)):d==="LINK"&&(c=a.href);Fb(document)&&a.setAttribute("nonce",Fb(document));return c?(a=Y.getEntriesByName(c))&&a[0]&&(a=a[0],c=ax(),Z("rsf_"+b,c+Math.round(a.fetchStart)),Z("rse_"+b,c+Math.round(a.responseEnd)),a.transferSize!==void 0&&a.transferSize===0)?!0:!1:!1}
function kx(){var a=window.location.protocol,b=Y.getEntriesByType("resource");b=Tb(b,function(c){return c.name.indexOf(a+"//fonts.gstatic.com/s/")===0});
(b=Vb(b,function(c,d){return d.duration>c.duration?d:c},{duration:0}))&&b.startTime>0&&b.responseEnd>0&&(Z("wffs",$w(b.startTime)),Z("wffe",$w(b.responseEnd)))}
function lx(a){var b=mx("aft",a);if(b)return b;b=P((a||"")+"TIMING_AFT_KEYS",["ol"]);for(var c=b.length,d=0;d<c;d++){var e=mx(b[d],a);if(e)return e}return NaN}
function mx(a,b){if(a=Jw(b)[a])return typeof a==="number"?a:a[a.length-1]}
function gx(a){var b=mx("_start",a),c=lx(a),d=R("enable_cow_info_csi")||!bx;b&&c&&d&&(Lq(Sw,new Rw(Math.round(c-b),a)),bx=!0)}
function nx(){if(Y.getEntriesByType){var a=Y.getEntriesByType("paint");if(a=Wb(a,function(c){return c.name==="first-paint"}))return $w(a.startTime)}var b;
R("csi_use_performance_navigation_timing")||R("csi_use_performance_navigation_timing_tvhtml5")?b=Y.getEntriesByType("first-paint")[0].startTime:b=Y.timing.Mh;return b?Math.max(0,b):0}
;function ox(a,b){um(function(){Ow("").info.actionType=a;b&&qm("TIMING_AFT_KEYS",b);qm("TIMING_ACTION",a);var c=ix();Object.keys(c).length>0&&ex(c);c={isNavigation:!0,actionType:Qw[P("TIMING_ACTION")]||"LATENCY_ACTION_UNKNOWN"};var d=P("PREVIOUS_ACTION");d&&(c.previousAction=Qw[d]||"LATENCY_ACTION_UNKNOWN");if(d=P("CLIENT_PROTOCOL"))c.httpProtocol=d;if(d=P("CLIENT_TRANSPORT"))c.transportProtocol=d;(d=Ou())&&d!=="UNDEFINED_CSN"&&(c.clientScreenNonce=d);d=hx();if(d===1||d===-1)c.isVisible=!0;Iw();Hw();
c.loadType="cold";d=Hw();var e=Zw(),f=ax(),g=P("CSI_START_TIMESTAMP_MILLIS",0);g>0&&!R("embeds_web_enable_csi_start_override_killswitch")&&(f=g);f&&(Z("srt",e.responseStart),d.prerender!==1&&Z("_start",f,void 0));d=nx();d>0&&Z("fpt",d);d=Zw();d.isPerformanceNavigationTiming&&ex({performanceNavigationTiming:!0},void 0);Z("nreqs",d.requestStart,void 0);Z("nress",d.responseStart,void 0);Z("nrese",d.responseEnd,void 0);d.redirectEnd-d.redirectStart>0&&(Z("nrs",d.redirectStart,void 0),Z("nre",d.redirectEnd,
void 0));d.domainLookupEnd-d.domainLookupStart>0&&(Z("ndnss",d.domainLookupStart,void 0),Z("ndnse",d.domainLookupEnd,void 0));d.connectEnd-d.connectStart>0&&(Z("ntcps",d.connectStart,void 0),Z("ntcpe",d.connectEnd,void 0));d.secureConnectionStart>=ax()&&d.connectEnd-d.secureConnectionStart>0&&(Z("nstcps",d.secureConnectionStart,void 0),Z("ntcpe",d.connectEnd,void 0));Y&&"getEntriesByType"in Y&&kx();d=[];if(document.querySelector&&Y&&Y.getEntriesByName)for(var h in dx)dx.hasOwnProperty(h)&&(e=dx[h],
jx(h,e)&&d.push(e));if(d.length>0)for(c.resourceInfo=[],h=y(d),d=h.next();!d.done;d=h.next())c.resourceInfo.push({resourceCache:d.value});ex(c);c=Kw();c.preLoggedGelInfos||(c.preLoggedGelInfos=[]);h=c.preLoggedGelInfos;c=Lw();d=void 0;for(e=0;e<h.length;e++)if(f=h[e],f.loadType){d=f.loadType;break}if(Iw().loadType==="cold"&&(c.loadType==="cold"||d==="cold")){d=Jw();e=Kw();e=e.gelTicks?e.gelTicks:e.gelTicks={};for(var k in d)if(!(k in e))if(typeof d[k]==="number")Z(k,mx(k));else if(R("log_repeated_ytcsi_ticks"))for(f=
y(d[k]),g=f.next();!g.done;g=f.next())g=g.value,Z(k.slice(1),g);k={};d=!1;h=y(h);for(e=h.next();!e.done;e=h.next())d=e.value,Jv(c,d),Jv(k,d),d=!0;d&&ex(k)}D("ytglobal.timingready_",!0);k=P("TIMING_ACTION");E("ytglobal.timingready_")&&k&&px()&&lx()&&gx()})()}
function px(a){return um(function(){return qx("_start",a)})()}
function rx(a,b,c){um(ex)(a,b,c===void 0?!1:c)}
function sx(a,b,c){return um(Z)(a,b,c)}
function qx(a,b){return um(function(){var c=Jw(b);return a in c})()}
function tx(a){if(!R("universal_csi_network_ticks"))return"";a=fc(hc(5,a))||"";for(var b=Object.keys(Dq),c=0;c<b.length;c++){var d=b[c];if(a.includes(d))return d}return""}
function ux(a){if(!R("universal_csi_network_ticks"))return function(){};
var b=Dq[a];return b?(vx(b),function(){var c=R("universal_csi_network_ticks")?(c=Eq[a])?vx(c):!1:!1;return c}):function(){}}
function vx(a){return um(function(){if(qx(a))return!1;sx(a,void 0,void 0);return!0})()}
function wx(a){um(function(){if(!px("attestation_challenge_fetch")||qx(a,"attestation_challenge_fetch"))return!1;sx(a,void 0,"attestation_challenge_fetch");return!0})()}
function xx(){um(function(){var a=Mw();requestAnimationFrame(function(){setTimeout(function(){a===Mw()&&sx("ol",void 0,void 0)},0)})})()}
var yx=window;yx.ytcsi&&(yx.ytcsi.infoGel=rx,yx.ytcsi.tick=sx);var zx="tokens consistency mss client_location entities adblock_detection response_received_commands store PLAYER_PRELOAD shorts_prefetch".split(" "),Ax=["type.googleapis.com/youtube.api.pfiinnertube.YoutubeApiInnertube.BrowseResponse","type.googleapis.com/youtube.api.pfiinnertube.YoutubeApiInnertube.PlayerResponse"];function Bx(a,b,c,d){this.u=a;this.fa=b;this.j=c;this.o=d;this.i=void 0;this.h=new Map;a.Xb||(a.Xb={});a.Xb=Object.assign({},Fw,a.Xb)}
function Cx(a,b,c,d){if(Bx.h!==void 0){if(d=Bx.h,a=[a!==d.u,b!==d.fa,c!==d.j,!1,!1,!1,void 0!==d.i],a.some(function(e){return e}))throw new T("InnerTubeTransportService is already initialized",a);
}else Bx.h=new Bx(a,b,c,d)}
function Dx(a){var b={signalServiceEndpoint:{signal:"GET_DATASYNC_IDS"}};var c=c===void 0?zn:c;var d=Ex(a,b);return d?new ui(function(e,f){var g,h,k,l,m;return A(function(n){switch(n.h){case 1:return n.yield(d,2);case 2:g=n.i;h=g.u(b,void 0,c);if(!h){f(new T("Error: Failed to build request for command.",b));n.A(0);break}hw(h.input);l=((k=h.Za)==null?void 0:k.mode)==="cors"?"cors":void 0;if(a.j.Md){m=Fx(h.config,l);n.A(4);break}return n.yield(Gx(h.config,l),5);case 5:m=n.i;case 4:e(Hx(a,h,m)),n.h=
0}})}):zi(new T("Error: No request builder found for command.",b))}
function Ix(a,b){function c(){}
var d="/youtubei/v1/"+Aw(pv);var e=e===void 0?{Mb:{identity:zn}}:e;var f=f===void 0?!0:f;c=ux(tx(d));b.context||(b.context=tw(void 0,f));return new ui(function(g){var h,k,l,m,n;return A(function(p){if(p.h==1)return h=Yu(d),k=Im(h)?"same-origin":"cors",a.j.Md?(l=Fx(e,k),p.A(2)):p.yield(Gx(e,k),3);p.h!=2&&(l=p.i);m=Zu(Yu(d));n={input:m,Za:$u(m),Ga:b,config:e};g(Hx(a,n,l,c));p.h=0})})}
function Jx(a,b,c){var d;if(b&&!(b==null?0:(d=b.sequenceMetaData)==null?0:d.skipProcessing)&&a.o){d=y(zx);for(var e=d.next();!e.done;e=d.next())e=e.value,a.o[e]&&a.o[e].handleResponse(b,c)}}
function Hx(a,b,c,d){d=d===void 0?function(){}:d;
var e,f,g,h,k,l,m,n,p,t,u,x,z,H,K,da,ea,Oa,Ob,ka,Ya,Qa,Ra,Pa,jh,kh,xs,ys,zs,As;return A(function(ja){switch(ja.h){case 1:ja.A(2);break;case 3:if((e=ja.i)&&!e.isExpired())return ja.return(Promise.resolve(e.h()));case 2:if(!((f=b)==null?0:(g=f.Ga)==null?0:g.context)){ja.A(4);break}h=b.Ga.context;ja.A(5);break;case 5:k=y([]),l=k.next();case 8:if(l.done){ja.A(4);break}m=l.value;return ja.yield(m.Nh(h),9);case 9:l=k.next();ja.A(8);break;case 4:if((n=a.i)==null||!n.Th(b.input,b.Ga)){ja.A(12);break}return ja.yield(a.i.Ih(b.input,
b.Ga),13);case 13:return p=ja.i,Jx(a,p,b),ja.return(p);case 12:return(x=(u=b.config)==null?void 0:u.Qh)&&a.h.has(x)?t=a.h.get(x):(z=JSON.stringify(b.Ga),da=(K=(H=b.Za)==null?void 0:H.headers)!=null?K:{},b.Za=Object.assign({},b.Za,{headers:Object.assign({},da,c)}),ea=Object.assign({},b.Za),b.Za.method==="POST"&&(ea=Object.assign({},ea,{body:z})),((Oa=b.config)==null?0:Oa.Ye)&&sx(b.config.Ye),Ob=function(){return a.fa.fetch(b.input,ea,b.config)},t=Ob(),x&&a.h.set(x,t)),ja.yield(t,14);
case 14:if((ka=ja.i)&&"error"in ka&&((Ya=ka)==null?0:(Qa=Ya.error)==null?0:Qa.details))for(Ra=ka.error.details,Pa=y(Ra),jh=Pa.next();!jh.done;jh=Pa.next())kh=jh.value,(xs=kh["@type"])&&Ax.indexOf(xs)>-1&&(delete kh["@type"],ka=kh);x&&a.h.has(x)&&a.h.delete(x);((ys=b.config)==null?0:ys.Ze)&&sx(b.config.Ze);if(ka||(zs=a.i)==null||!zs.wh(b.input,b.Ga)){ja.A(15);break}return ja.yield(a.i.Hh(b.input,b.Ga),16);case 16:ka=ja.i;case 15:return Jx(a,ka,b),((As=b.config)==null?0:As.Ve)&&sx(b.config.Ve),d(),
ja.return(ka||void 0)}})}
function Ex(a,b){a:{a=a.u;var c,d=(c=Gt(b,Yl))==null?void 0:c.signal;if(d&&a.Xb&&(c=a.Xb[d])){var e=c();break a}var f;if((c=(f=Gt(b,Wl))==null?void 0:f.request)&&a.je&&(f=a.je[c])){e=f();break a}for(e in b)if(a.qd[e]&&(b=a.qd[e])){e=b();break a}e=void 0}if(e!==void 0)return Promise.resolve(e)}
function Gx(a,b){var c,d,e,f;return A(function(g){if(g.h==1){e=(c=a)==null?void 0:(d=c.Mb)==null?void 0:d.sessionIndex;var h=g.yield;var k=yn(0,{sessionIndex:e});if(!(k instanceof ui)){var l=new ui(si);vi(l,2,k);k=l}return h.call(g,k,2)}f=g.i;return g.return(Promise.resolve(Object.assign({},uw(b),f)))})}
function Fx(a,b){var c;a=a==null?void 0:(c=a.Mb)==null?void 0:c.sessionIndex;c=yn(0,{sessionIndex:a});return Object.assign({},uw(b),c)}
;var Kx=new Zs("INNERTUBE_TRANSPORT_TOKEN");function Lx(){}
w(Lx,Cw);Lx.prototype.j=function(){return vv};
Lx.prototype.i=function(a){return Gt(a,hm)||void 0};
Lx.prototype.h=function(a,b,c){c=c===void 0?{}:c;b.channelIds&&(a.channelIds=b.channelIds);b.siloName&&(a.siloName=b.siloName);b.params&&(a.params=b.params);c.botguardResponse&&(a.botguardResponse=c.botguardResponse);c.feature&&(a.clientFeature=c.feature)};
fa.Object.defineProperties(Lx.prototype,{o:{configurable:!0,enumerable:!0,get:function(){return!0}}});function Mx(){}
w(Mx,Cw);Mx.prototype.j=function(){return wv};
Mx.prototype.i=function(a){return Gt(a,gm)||void 0};
Mx.prototype.h=function(a,b){b.channelIds&&(a.channelIds=b.channelIds);b.siloName&&(a.siloName=b.siloName);b.params&&(a.params=b.params)};
fa.Object.defineProperties(Mx.prototype,{o:{configurable:!0,enumerable:!0,get:function(){return!0}}});var Nx=new Zs("SHARE_CLIENT_PARAMS_PROVIDER_TOKEN");function Ox(a){this.M=a}
w(Ox,Cw);Ox.prototype.j=function(){return qv};
Ox.prototype.i=function(a){return Gt(a,bm)||Gt(a,cm)||Gt(a,am)};
Ox.prototype.h=function(a,b){b.serializedShareEntity&&(a.serializedSharedEntity=b.serializedShareEntity);if(b.clientParamIdentifier){var c;if((c=this.M)==null?0:c.h(b.clientParamIdentifier))a.clientParams=this.M.i(b.clientParamIdentifier)}};
Ox[Ys]=[Nx];function Px(){}
w(Px,Cw);Px.prototype.j=function(){return sv};
Px.prototype.i=function(a){return Gt(a,$l)||void 0};
Px.prototype.h=function(a,b,c){a.feedbackTokens=[];b.feedbackToken&&a.feedbackTokens.push(b.feedbackToken);if(b=b.cpn||c.cpn)a.feedbackContext={cpn:b};a.isFeedbackTokenUnencrypted=!!c.is_feedback_token_unencrypted;a.shouldMerge=!1;c.extra_feedback_tokens&&(a.shouldMerge=!0,a.feedbackTokens=a.feedbackTokens.concat(c.extra_feedback_tokens))};
fa.Object.defineProperties(Px.prototype,{o:{configurable:!0,enumerable:!0,get:function(){return!0}}});function Qx(){}
w(Qx,Cw);Qx.prototype.j=function(){return tv};
Qx.prototype.i=function(a){return Gt(a,fm)||void 0};
Qx.prototype.h=function(a,b){b.params&&(a.params=b.params);b.secondaryParams&&(a.secondaryParams=b.secondaryParams)};function Rx(){}
w(Rx,Cw);Rx.prototype.j=function(){return uv};
Rx.prototype.i=function(a){return Gt(a,em)||void 0};
Rx.prototype.h=function(a,b){b.actions&&(a.actions=b.actions);b.params&&(a.params=b.params);b.playlistId&&(a.playlistId=b.playlistId)};function Sx(){}
w(Sx,Cw);Sx.prototype.j=function(){return rv};
Sx.prototype.i=function(a){return Gt(a,dm)};
Sx.prototype.h=function(a,b,c){c=c===void 0?{}:c;b.serializedShareEntity&&(a.serializedSharedEntity=b.serializedShareEntity);c.includeListId&&(a.includeListId=!0)};var Tx=new Zs("FETCH_FN_TOKEN"),Ux=new Zs("PARSE_FN_TOKEN");function Vx(a,b){var c=B.apply(2,arguments);a=a===void 0?0:a;T.call(this,b,c);this.errorType=a;Object.setPrototypeOf(this,this.constructor.prototype)}
w(Vx,T);var Wx=new Zs("NETWORK_SLI_TOKEN");function Xx(a,b,c){this.h=a;this.i=b;this.j=c}
Xx.prototype.fetch=function(a,b,c){var d=this,e,f,g;return A(function(h){e=Yx(d,a,b);g=(f=d.i)!=null?f:fetch;return h.return(g(e).then(function(k){return d.handleResponse(k,c)}).catch(function(k){V(k);
if((c==null?0:c.se)&&k instanceof Vx&&k.errorType===1)return Promise.reject(k)}))})};
function Yx(a,b,c){if(a.h){var d=fc(hc(5,rc(b,"key")))||"/UNKNOWN_PATH";a.h.start(d)}a=c;R("wug_networking_gzip_request")&&(a=mr(c));return new window.Request(b,a)}
Xx.prototype.handleResponse=function(a,b){var c,d=(c=this.j)!=null?c:JSON.parse;c=a.text().then(function(e){if((b==null?0:b.Ie)&&a.ok)return Sf(b.Ie,e);e=e.replace(")]}'","");if((b==null?0:b.se)&&e)try{var f=d(e)}catch(h){throw new Vx(1,"JSON parsing failed after fetch");}var g;return(g=f)!=null?g:d(e)});
a.redirected||a.ok?this.h&&this.h.success():(this.h&&this.h.Ch(),c=c.then(function(e){V(new T("Error: API fetch failed",a.status,a.url,e));return Object.assign({},e,{errorMetadata:{status:a.status}})}));
return c};
Xx[Ys]=[new $s(Wx),new $s(Tx),new $s(Ux)];var Zx=new Zs("NETWORK_MANAGER_TOKEN");var $x;function ay(){var a,b;if(!$x){var c=ft();bt(c,{zc:Zx,Pd:Xx});var d={qd:{feedbackEndpoint:xw(Px),modifyChannelNotificationPreferenceEndpoint:xw(Qx),playlistEditEndpoint:xw(Rx),shareEntityEndpoint:xw(Ox),subscribeEndpoint:xw(Lx),unsubscribeEndpoint:xw(Mx),webPlayerShareEntityServiceEndpoint:xw(Sx)}},e=sw(),f={};e&&(f.client_location=e);a===void 0&&(a=xn());b===void 0&&(b=c.resolve(Zx));Cx(d,b,a,f);bt(c,{zc:Kx,Qd:Bx.h});$x=c.resolve(Kx)}return $x}
;function by(a){var b=new tj;if(a.interpreterJavascript){var c=Ol(a.interpreterJavascript);c=Ib(c).toString();var d=new rj;Bf(d,6,c);uf(b,rj,1,d)}else a.interpreterUrl&&(c=Pl(a.interpreterUrl),c=pb(c).toString(),d=new sj,Bf(d,4,c),uf(b,sj,2,d));a.interpreterHash&&Cf(b,3,a.interpreterHash);a.program&&Cf(b,4,a.program);a.globalName&&Cf(b,5,a.globalName);a.clientExperimentsStateBlob&&Cf(b,7,a.clientExperimentsStateBlob);return b}
function cy(a){var b={};a=y(a.split("&"));for(var c=a.next();!c.done;c=a.next())c=c.value.split("="),c.length===2&&(b[c[0]]=c[1]);return b}
;function Cj(){if(R("bg_st_hr"))return"havuokmhhs-0";var a,b=((a=performance)==null?void 0:a.timeOrigin)||0;return"havuokmhhs-"+Math.floor(b)}
function dy(a){this.h=a}
dy.prototype.bindInnertubeChallengeFetcher=function(a){this.h.bicf(a)};
dy.prototype.registerChallengeFetchedCallback=function(a){this.h.bcr(a)};
dy.prototype.getLatestChallengeResponse=function(){return this.h.blc()};
function ey(){return new Promise(function(a){var b=window.top;b.ntpevasrs!==void 0?a(new dy(b.ntpevasrs)):(b.ntpqfbel===void 0&&(b.ntpqfbel=[]),b.ntpqfbel.push(function(c){a(new dy(c))}))})}
;var fy=[],gy=!1;function hy(){if(!R("disable_biscotti_fetch_for_ad_blocker_detection")&&!R("disable_biscotti_fetch_entirely_for_all_web_clients")&&av()){var a=P("PLAYER_VARS",{});if(yg(a)!="1"&&!bv(a)){var b=function(){gy=!0;"google_ad_status"in window?qm("DCLKSTAT",1):qm("DCLKSTAT",2)};
try{zv("//static.doubleclick.net/instream/ad_status.js",b)}catch(c){}fy.push(ak.ma(function(){if(!(gy||"google_ad_status"in window)){try{Dv("//static.doubleclick.net/instream/ad_status.js",b)}catch(c){}gy=!0;qm("DCLKSTAT",3)}},5E3))}}}
function iy(){var a=Number(P("DCLKSTAT",0));return isNaN(a)?0:a}
;function jy(a){this.h=a}
[new jy("b.f_"),new jy("j.s_"),new jy("r.s_"),new jy("e.h_"),new jy("i.s_"),new jy("s.t_"),new jy("p.h_"),new jy("s.i_"),new jy("f.i_"),new jy("a.b_"),new jy("a.o_"),new jy("g.o_"),new jy("p.i_"),new jy("p.m_"),new jy("i.k_"),new jy("n.k_"),new jy("i.f_"),new jy("a.s_"),new jy("m.c_"),new jy("n.h_"),new jy("o.p_")].reduce(function(a,b){a[b.h]=b;return a},{});function ky(a,b){var c={preload:!0},d=this;this.network=a;this.options=c;this.o=b;this.h=null;if(c.Yh){var e=new yj;this.h=e.promise;C.ytAtRC&&ak.Ra(function(){var f,g;return A(function(h){if(h.h==1){if(!C.ytAtRC)return h.return();f=ly(null);return h.yield(d.gb(f),2)}g=h.i;C.ytAtRC&&C.ytAtRC(JSON.stringify(g));h.h=0})},2);
ey().then(function(f){var g,h,k,l;return A(function(m){if(m.h==1)return f.bindInnertubeChallengeFetcher(function(n){return d.gb(ly(n))}),m.yield(Bj(),2);
g=m.i;h=f.getLatestChallengeResponse();k=h.challenge;if(!k)throw Error("BGE_MACIL");l={challenge:k,eb:cy(k),vm:g,bgChallenge:new tj};e.resolve(l);f.registerChallengeFetchedCallback(function(n){n=n.challenge;if(!n)throw Error("BGE_MACR");n={challenge:n,eb:cy(n),vm:g,bgChallenge:new tj};d.h=Promise.resolve(n)});
m.h=0})})}else c.preload&&my(this,new Promise(function(f){Un(function(){f(ny(d))},0)}))}
ky.prototype.j=function(){var a=this;return A(function(b){return b.h==1?b.yield(Promise.race([a.h,null]),2):b.return(!!b.i)})};
ky.prototype.i=function(a,b,c){var d=this,e,f,g;return A(function(h){d.h===null&&my(d,ny(d));e=!1;f={};g=function(){var k,l,m;return A(function(n){switch(n.h){case 1:return n.yield(d.h,2);case 2:k=n.i;f.challenge=k.challenge;if(!k.vm){"c1a"in k.eb&&(f.error="ATTESTATION_ERROR_VM_NOT_INITIALIZED");n.A(3);break}l=Object.assign({},{c:k.challenge,e:a},b);za(n,4);e=!0;if(R("attbs")&&!R("attmusi")){m=k.vm.gd({vb:l});n.A(6);break}return n.yield(k.vm.snapshot({vb:l}),7);case 7:m=n.i;case 6:m?f.webResponse=
m:f.error="ATTESTATION_ERROR_VM_NO_RESPONSE";Aa(n,3);break;case 4:Ba(n),f.error="ATTESTATION_ERROR_VM_INTERNAL_ERROR";case 3:if(a==="ENGAGEMENT_TYPE_PLAYBACK"){var p=k.eb,t={};p.c6a&&(t.reportingStatus=String(Number(p.c)^iy()));p.c6b&&(t.broadSpectrumDetectionResult=String(Number(p.c)^Number(P("CATSTAT",0))));f.adblockReporting=t}return n.return(f)}})};
return h.return(Promise.race([g(),oy(c,function(){var k=Object.assign({},f);e&&(k.error="ATTESTATION_ERROR_VM_TIMEOUT");return k})]))})};
function ly(a){var b={engagementType:"ENGAGEMENT_TYPE_UNBOUND"};a&&(b.interpreterHash=a);return b}
function ny(a,b){b=b===void 0?0:b;var c,d,e,f,g,h,k,l,m,n,p,t;return A(function(u){switch(u.h){case 1:c=ly(Hj().h);if(R("att_fet_ks"))return za(u,7),u.yield(a.gb(c),9);za(u,4);return u.yield(py(a,c),6);case 6:g=u.i;e=g.Qe;f=g.Re;d=g;Aa(u,3);break;case 4:return Ba(u),V(Error("Failed to fetch attestation challenge after "+(b+" attempts; not retrying for 24h."))),qy(a,864E5),u.return({challenge:"",eb:{},vm:void 0,bgChallenge:void 0});case 9:d=u.i;if(!d)throw Error("Fetching Attestation challenge returned falsy");
if(!d.challenge)throw Error("Missing Attestation challenge");e=d.challenge;f=cy(e);if("c1a"in f&&(!d.bgChallenge||!d.bgChallenge.program))throw Error("Expected bg challenge but missing.");Aa(u,3);break;case 7:h=Ba(u);V(h);b++;if(b>=5)return V(Error("Failed to fetch attestation challenge after "+(b+" attempts; not retrying for 24h."))),qy(a,864E5),u.return({challenge:"",eb:{},vm:void 0,bgChallenge:void 0});k=1E3*Math.pow(2,b-1)+Math.random()*1E3;return u.return(new Promise(function(x){Un(function(){x(ny(a,
b))},k)}));
case 3:l=Number(f.t)||7200;qy(a,l*1E3);m=void 0;if(!("c1a"in f&&d.bgChallenge)){u.A(10);break}n=by(d.bgChallenge);za(u,11);return u.yield(Ij(Hj(),n),13);case 13:Aa(u,12);break;case 11:return p=Ba(u),V(p),u.return({challenge:e,eb:f,vm:m,bgChallenge:n});case 12:return za(u,14),m=new Aj({challenge:n,Ad:{Da:"aGIf"}}),u.yield(m.bd,16);case 16:Aa(u,10);break;case 14:t=Ba(u),V(t),m=void 0;case 10:return u.return({challenge:e,eb:f,vm:m,bgChallenge:n})}})}
ky.prototype.gb=function(a){var b=this,c;return A(function(d){c=b.o;if(!c||c.ta())return d.return(b.network.gb(a));wx("att_pna");return d.return(new Promise(function(e){$h(c,"publicytnetworkstatus-online",function(){b.network.gb(a).then(e)})}))})};
function ry(a){if(!a)throw Error("Fetching Attestation challenge returned falsy");if(!a.challenge)throw Error("Missing Attestation challenge");var b=a.challenge,c=cy(b);if("c1a"in c&&(!a.bgChallenge||!a.bgChallenge.program))throw Error("Expected bg challenge but missing.");return Object.assign({},a,{Qe:b,Re:c})}
function py(a,b){var c,d,e,f,g;return A(function(h){switch(h.h){case 1:c=void 0,d=0,e={};case 2:if(!(d<5)){h.A(4);break}if(!(d>0)){h.A(5);break}e.nd=1E3*Math.pow(2,d-1)+Math.random()*1E3;return h.yield(new Promise(function(k){return function(l){Un(function(){l(void 0)},k.nd)}}(e)),5);
case 5:return za(h,7),h.yield(a.gb(b),9);case 9:return f=h.i,h.return(ry(f));case 7:c=g=Ba(h),g instanceof Error&&V(g);case 8:d++;e={nd:void 0};h.A(2);break;case 4:throw c;}})}
function my(a,b){a.h=b}
function sy(a){var b,c,d;return A(function(e){if(e.h==1)return e.yield(Promise.race([a.h,null]),2);b=e.i;var f=ny(a);a.h=f;(c=b)==null||(d=c.vm)==null||d.dispose();e.h=0})}
function qy(a,b){function c(){var e;return A(function(f){e=d-Date.now();return e<1E3?f.yield(sy(a),0):(Un(c,Math.min(e,6E4)),f.A(0))})}
var d=Date.now()+b;c()}
function oy(a,b){return new Promise(function(c){Un(function(){c(b())},a)})}
;function ty(){this.h=ay()}
ty.prototype.gb=function(a){wx("att_fsr");return Ix(this.h,a).then(function(b){wx("att_frr");return b})};function uy(){var a,b,c;return A(function(d){if(d.h==1)return a=ft().resolve(Kx),a?d.yield(Dx(a),2):(V(Error("InnertubeTransportService unavailable in fetchDatasyncIds")),d.return(void 0));if(b=d.i){if(b.errorMetadata)return V(Error("Datasync IDs fetch responded with "+b.errorMetadata.status+": "+b.error)),d.return(void 0);c=b.zh;return d.return(c)}V(Error("Network request to get Datasync IDs failed."));return d.return(void 0)})}
;function vy(){var a;return(a=P("WEB_PLAYER_CONTEXT_CONFIGS"))==null?void 0:a.WEB_PLAYER_CONTEXT_CONFIG_ID_EMBEDDED_PLAYER}
;var wy=C.caches,xy;function yy(a){var b=a.indexOf(":");return b===-1?{Dd:a}:{Dd:a.substring(0,b),datasyncId:a.substring(b+1)}}
function zy(){return A(function(a){if(xy!==void 0)return a.return(xy);xy=new Promise(function(b){var c;return A(function(d){switch(d.h){case 1:return za(d,2),d.yield(wy.open("test-only"),4);case 4:return d.yield(wy.delete("test-only"),5);case 5:Aa(d,3);break;case 2:if(c=Ba(d),c instanceof Error&&c.name==="SecurityError")return b(!1),d.return();case 3:b("caches"in window),d.h=0}})});
return a.return(xy)})}
function Ay(a){var b,c,d,e,f,g,h;A(function(k){if(k.h==1)return k.yield(zy(),2);if(k.h!=3){if(!k.i)return k.return(!1);b=[];return k.yield(wy.keys(),3)}c=k.i;d=y(c);for(e=d.next();!e.done;e=d.next())f=e.value,g=yy(f),h=g.datasyncId,!h||a.includes(h)||b.push(wy.delete(f));return k.return(Promise.all(b).then(function(l){return l.some(function(m){return m})}))})}
function By(){var a,b,c,d,e,f,g;return A(function(h){if(h.h==1)return h.yield(zy(),2);if(h.h!=3){if(!h.i)return h.return(!1);a=Sn("cache contains other");return h.yield(wy.keys(),3)}b=h.i;c=y(b);for(d=c.next();!d.done;d=c.next())if(e=d.value,f=yy(e),(g=f.datasyncId)&&g!==a)return h.return(!0);return h.return(!1)})}
;function Cy(){try{return!!self.sessionStorage}catch(a){return!1}}
;function Dy(a){a=a.match(/(.*)::.*::.*/);if(a!==null)return a[1]}
function Ey(a){if(Cy()){var b=Object.keys(window.sessionStorage);b=y(b);for(var c=b.next();!c.done;c=b.next()){c=c.value;var d=Dy(c);d===void 0||a.includes(d)||self.sessionStorage.removeItem(c)}}}
function Fy(){if(!Cy())return!1;var a=Sn(),b=Object.keys(window.sessionStorage);b=y(b);for(var c=b.next();!c.done;c=b.next())if(c=Dy(c.value),c!==void 0&&c!==a)return!0;return!1}
;function Gy(){uy().then(function(a){a&&(Yp(a),Ay(a),ew(a),Ey(a))})}
function Hy(){var a=new es;ak.ma(function(){var b,c,d,e,f;return A(function(g){switch(g.h){case 1:if(R("ytidb_clear_optimizations_killswitch")){g.A(2);break}b=Sn("clear");if(b.startsWith("V")&&b.endsWith("||")){var h=[b];Yp(h);Ay(h);ew(h);Ey(h);return g.return()}c=fw();d=Fy();return g.yield(By(),3);case 3:return e=g.i,g.yield(Zp(),4);case 4:if(f=g.i,!(c||d||e||f))return g.return();case 2:a.ta()?Gy():$h(a,"publicytnetworkstatus-online",Gy),g.h=0}})})}
;function Iy(){this.state=1;this.vm=null}
r=Iy.prototype;r.initialize=function(a,b,c){if(a.program){var d,e=(d=a.interpreterUrl)!=null?d:null;if(a.interpreterSafeScript)d=Ol(a.interpreterSafeScript);else{var f;d=(f=a.interpreterScript)!=null?f:null}a.interpreterSafeUrl&&(e=Pl(a.interpreterSafeUrl).toString());Jy(this,d,e,a.program,b,c)}else V(Error("Cannot initialize botguard without program"))};
function Jy(a,b,c,d,e,f){var g=g===void 0?"trayride":g;c?(a.state=2,zv(c,function(){window[g]?Ky(a,d,g,e):(a.state=3,Bv(c),V(new T("Unable to load Botguard","from "+c)))},f)):b?(f=Hg("SCRIPT"),b instanceof Gb?(f.textContent=Ib(b),Jb(f)):f.textContent=b,f.nonce=Fb(document),document.head.appendChild(f),document.head.removeChild(f),window[g]?Ky(a,d,g,e):(a.state=4,V(new T("Unable to load Botguard from JS")))):V(new T("Unable to load VM; no url or JS provided"))}
r.isLoading=function(){return this.state===2};
function Ky(a,b,c,d){a.state=5;try{var e=new Aj({program:b,globalName:c,Ad:{disable:!R("att_web_record_metrics"),Da:"aGIf"}});e.bd.then(function(){a.state=6;d&&d(b)});
a.Zc(e)}catch(f){a.state=7,f instanceof Error&&V(f)}}
r.invoke=function(a){a=a===void 0?{}:a;return this.jd()?this.Sd({vb:a}):null};
r.dispose=function(){this.Zc(null);this.state=8};
r.jd=function(){return!!this.vm};
r.Sd=function(a){return this.vm.gd(a)};
r.Zc=function(a){zc(this.vm);this.vm=a};function Ly(){var a=E("yt.abuse.playerAttLoader");return a&&["bgvma","bgvmb","bgvmc"].every(function(b){return b in a})?a:null}
;function My(){Iy.apply(this,arguments)}
w(My,Iy);My.prototype.Zc=function(a){var b;(b=Ly())==null||b.bgvma();a?(b={bgvma:a.dispose.bind(a),bgvmb:a.snapshot.bind(a),bgvmc:a.gd.bind(a)},D("yt.abuse.playerAttLoader",b),D("yt.abuse.playerAttLoaderRun",function(c){return a.snapshot(c)})):(D("yt.abuse.playerAttLoader",null),D("yt.abuse.playerAttLoaderRun",null))};
My.prototype.jd=function(){return!!Ly()};
My.prototype.Sd=function(a){return Ly().bgvmc(a)};function Ny(a){qt.call(this,a===void 0?"document_active":a);var b=this;this.o=10;this.h=new Map;this.transitions=[{from:"document_active",to:"document_disposed_preventable",action:this.G},{from:"document_active",to:"document_disposed",action:this.u},{from:"document_disposed_preventable",to:"document_disposed",action:this.u},{from:"document_disposed_preventable",to:"flush_logs",action:this.M},{from:"document_disposed_preventable",to:"document_active",action:this.i},{from:"document_disposed",to:"flush_logs",
action:this.M},{from:"document_disposed",to:"document_active",action:this.i},{from:"document_disposed",to:"document_disposed",action:function(){}},
{from:"flush_logs",to:"document_active",action:this.i}];window.addEventListener("pagehide",function(c){b.transition("document_disposed",{event:c})});
window.addEventListener("beforeunload",function(c){b.transition("document_disposed_preventable",{event:c})})}
w(Ny,qt);Ny.prototype.G=function(a,b){if(!this.h.get("document_disposed_preventable")){a(b==null?void 0:b.event);var c,d;if((b==null?0:(c=b.event)==null?0:c.defaultPrevented)||(b==null?0:(d=b.event)==null?0:d.returnValue)){b.event.returnValue||(b.event.returnValue=!0);b.event.defaultPrevented||b.event.preventDefault();this.h=new Map;this.transition("document_active");return}}this.h.set("document_disposed_preventable",!0);this.h.get("document_disposed")?this.transition("flush_logs"):this.transition("document_disposed")};
Ny.prototype.u=function(a,b){this.h.get("document_disposed")?this.transition("document_active"):(a(b==null?void 0:b.event),this.h.set("document_disposed",!0),this.transition("flush_logs"))};
Ny.prototype.M=function(a,b){a(b==null?void 0:b.event);this.transition("document_active")};
Ny.prototype.i=function(){this.h=new Map};function Oy(a){qt.call(this,a===void 0?"document_visibility_unknown":a);var b=this;this.transitions=[{from:"document_visibility_unknown",to:"document_visible",action:this.i},{from:"document_visibility_unknown",to:"document_hidden",action:this.h},{from:"document_visibility_unknown",to:"document_foregrounded",action:this.M},{from:"document_visibility_unknown",to:"document_backgrounded",action:this.u},{from:"document_visible",to:"document_hidden",action:this.h},{from:"document_visible",to:"document_foregrounded",
action:this.M},{from:"document_visible",to:"document_visible",action:this.i},{from:"document_foregrounded",to:"document_visible",action:this.i},{from:"document_foregrounded",to:"document_hidden",action:this.h},{from:"document_foregrounded",to:"document_foregrounded",action:this.M},{from:"document_hidden",to:"document_visible",action:this.i},{from:"document_hidden",to:"document_backgrounded",action:this.u},{from:"document_hidden",to:"document_hidden",action:this.h},{from:"document_backgrounded",to:"document_hidden",
action:this.h},{from:"document_backgrounded",to:"document_backgrounded",action:this.u},{from:"document_backgrounded",to:"document_visible",action:this.i}];document.addEventListener("visibilitychange",function(c){document.visibilityState==="visible"?b.transition("document_visible",{event:c}):b.transition("document_hidden",{event:c})});
R("visibility_lifecycles_dynamic_backgrounding")&&(window.addEventListener("blur",function(c){b.transition("document_backgrounded",{event:c})}),window.addEventListener("focus",function(c){b.transition("document_foregrounded",{event:c})}))}
w(Oy,qt);Oy.prototype.i=function(a,b){a(b==null?void 0:b.event);R("visibility_lifecycles_dynamic_backgrounding")&&this.transition("document_foregrounded")};
Oy.prototype.h=function(a,b){a(b==null?void 0:b.event);R("visibility_lifecycles_dynamic_backgrounding")&&this.transition("document_backgrounded")};
Oy.prototype.u=function(a,b){a(b==null?void 0:b.event)};
Oy.prototype.M=function(a,b){a(b==null?void 0:b.event)};function Py(){this.o=new Ny;this.u=new Oy}
Py.prototype.install=function(){var a=B.apply(0,arguments),b=this;a.forEach(function(c){b.o.install(c)});
a.forEach(function(c){b.u.install(c)})};function Qy(){this.o=[];this.i=new Map;this.h=new Map;this.j=new Set}
Qy.prototype.clickCommand=function(a,b,c){var d=a.clickTrackingParams;c=c===void 0?0:c;if(d)if(c=Ou(c===void 0?0:c)){a=this.client;d=new Hu({trackingParams:d});var e=void 0;if(R("no_client_ve_attach_unless_shown")){var f=$v(d,c);Wv.set(f,!0);aw(d,c)}e=e||"INTERACTION_LOGGING_GESTURE_TYPE_GENERIC_CLICK";f=Zv({cttAuthInfo:Qu(c)||void 0},c);d={csn:c,ve:d.getAsJson(),gestureType:e};b&&(d.clientData=b);c==="UNDEFINED_CSN"?bw("visualElementGestured",f,d):a?ou("visualElementGestured",d,a,f):Jo("visualElementGestured",
d,f);b=!0}else b=!1;else b=!1;return b};
Qy.prototype.stateChanged=function(a,b,c){this.visualElementStateChanged(new Hu({trackingParams:a}),b,c===void 0?0:c)};
Qy.prototype.visualElementStateChanged=function(a,b,c){c=c===void 0?0:c;if(c===0&&this.j.has(c))this.o.push([a,b]);else{var d=c;d=d===void 0?0:d;c=Ou(d);a||(a=(a=Lu(d===void 0?0:d))?new Hu({veType:a,youtubeData:void 0,jspbYoutubeData:void 0}):null);var e=a;c&&e&&(a=this.client,d=Zv({cttAuthInfo:Qu(c)||void 0},c),b={csn:c,ve:e.getAsJson(),clientData:b},c==="UNDEFINED_CSN"?bw("visualElementStateChanged",d,b):a?ou("visualElementStateChanged",b,a,d):Jo("visualElementStateChanged",b,d))}};
function Ry(a,b){if(b===void 0)for(var c=Nu(),d=0;d<c.length;d++)c[d]!==void 0&&Ry(a,c[d]);else a.i.forEach(function(e,f){(f=a.h.get(f))&&Yv(a.client,b,f,e)}),a.i.clear(),a.h.clear()}
;function Sy(){Py.call(this);var a={};this.install((a.document_disposed={callback:this.h},a));R("combine_ve_grafts")&&(a={},this.install((a.document_disposed={callback:this.i},a)));a={};this.install((a.flush_logs={callback:this.j},a));R("web_log_cfg_cee_ks")||Un(Ty)}
w(Sy,Py);Sy.prototype.j=function(){Jo("finalPayload",{csn:Ou()})};
Sy.prototype.h=function(){Bu(Cu)};
Sy.prototype.i=function(){var a=Ry;Qy.h||(Qy.h=new Qy);a(Qy.h)};
function Ty(){var a=P("CLIENT_EXPERIMENT_EVENTS");if(a){var b=fe();a=y(a);for(var c=a.next();!c.done;c=a.next())c=c.value,b(c)&&Jo("genericClientExperimentEvent",{eventType:c});delete pm.CLIENT_EXPERIMENT_EVENTS}}
;function Uy(){}
function Vy(){var a=E("ytglobal.storage_");a||(a=new Uy,D("ytglobal.storage_",a));return a}
Uy.prototype.estimate=function(){var a,b,c;return A(function(d){a=navigator;return((b=a.storage)==null?0:b.estimate)?d.return(a.storage.estimate()):((c=a.webkitTemporaryStorage)==null?0:c.queryUsageAndQuota)?d.return(Wy()):d.return()})};
function Wy(){var a=navigator;return new Promise(function(b,c){var d;(d=a.webkitTemporaryStorage)!=null&&d.queryUsageAndQuota?a.webkitTemporaryStorage.queryUsageAndQuota(function(e,f){b({usage:e,quota:f})},function(e){c(e)}):c(Error("webkitTemporaryStorage is not supported."))})}
D("ytglobal.storageClass_",Uy);function Ho(a,b){var c=this;this.handleError=a;this.h=b;this.i=!1;self.document===void 0||self.addEventListener("beforeunload",function(){c.i=!0});
this.j=Math.random()<=.2}
Ho.prototype.Ha=function(a){this.handleError(a)};
Ho.prototype.logEvent=function(a,b){switch(a){case "IDB_DATA_CORRUPTED":R("idb_data_corrupted_killswitch")||this.h("idbDataCorrupted",b);break;case "IDB_UNEXPECTEDLY_CLOSED":this.h("idbUnexpectedlyClosed",b);break;case "IS_SUPPORTED_COMPLETED":R("idb_is_supported_completed_killswitch")||this.h("idbIsSupportedCompleted",b);break;case "QUOTA_EXCEEDED":Xy(this,b);break;case "TRANSACTION_ENDED":this.j&&Math.random()<=.1&&this.h("idbTransactionEnded",b);break;case "TRANSACTION_UNEXPECTEDLY_ABORTED":a=
Object.assign({},b,{hasWindowUnloaded:this.i}),this.h("idbTransactionAborted",a)}};
function Xy(a,b){Vy().estimate().then(function(c){c=Object.assign({},b,{isSw:self.document===void 0,isIframe:self!==self.top,deviceStorageUsageMbytes:Yy(c==null?void 0:c.usage),deviceStorageQuotaMbytes:Yy(c==null?void 0:c.quota)});a.h("idbQuotaExceeded",c)})}
function Yy(a){return typeof a==="undefined"?"-1":String(Math.ceil(a/1048576))}
;var Zy={},$y=(Zy["api.invalidparam"]=2,Zy.auth=150,Zy["drm.auth"]=150,Zy["heartbeat.net"]=150,Zy["heartbeat.servererror"]=150,Zy["heartbeat.stop"]=150,Zy["html5.unsupportedads"]=5,Zy["fmt.noneavailable"]=5,Zy["fmt.decode"]=5,Zy["fmt.unplayable"]=5,Zy["html5.missingapi"]=5,Zy["html5.unsupportedlive"]=5,Zy["drm.unavailable"]=5,Zy["mrm.blocked"]=151,Zy["embedder.identity.denied"]=152,Zy);var az=new Set("endSeconds startSeconds mediaContentUrl suggestedQuality videoId rct rctn playmuted muted_autoplay_duration_mode".split(" "));function bz(a){return(a.search("cue")===0||a.search("load")===0)&&a!=="loadModule"}
function cz(a,b,c){if(typeof a==="string")return{videoId:a,startSeconds:b,suggestedQuality:c};b={};c=y(az);for(var d=c.next();!d.done;d=c.next())d=d.value,a[d]&&(b[d]=a[d]);return b}
function dz(a,b,c,d){if(Sa(a)&&!Array.isArray(a)){b="playlist list listType index startSeconds suggestedQuality".split(" ");c={};for(d=0;d<b.length;d++){var e=b[d];a[e]&&(c[e]=a[e])}return c}b={index:b,startSeconds:c,suggestedQuality:d};typeof a==="string"&&a.length===16?b.list="PL"+a:b.playlist=a;return b}
;function ez(a){F.call(this);var b=this;this.api=a;this.Y=this.u=!1;this.D=[];this.P={};this.j=[];this.i=[];this.Z=!1;this.sessionId=this.h=null;this.targetOrigin="*";this.U=R("web_player_split_event_bus_iframe");this.o=P("POST_MESSAGE_ORIGIN")||document.location.protocol+"//"+document.location.hostname;this.G=function(c){a:if(!(b.o!=="*"&&c.origin!==b.o||b.h&&c.source!==b.h||typeof c.data!=="string")){try{var d=JSON.parse(c.data)}catch(h){break a}if(d)switch(d.event){case "listening":var e=c.source;
c=c.origin;d=d.id;c!=="null"&&(b.o=b.targetOrigin=c);b.h=e;b.sessionId=d;if(b.u){b.Y=!0;b.u=!1;b.sendMessage("initialDelivery",fz(b));b.sendMessage("onReady");e=y(b.D);for(d=e.next();!d.done;d=e.next())gz(b,d.value);b.D=[]}break;case "command":if(e=d.func,d=d.args,e==="addEventListener"&&d)e=d[0],d=c.origin,e==="onReady"?b.api.logApiCall(e+" invocation",d):e==="onError"&&b.Z&&(b.api.logApiCall(e+" invocation",d,b.errorCode),b.errorCode=void 0),b.api.logApiCall(e+" registration",d),b.P[e]||e==="onReady"||
(c=hz(b,e,d),b.i.push({eventType:e,listener:c,origin:d}),b.U?b.api.handleExternalCall("addEventListener",[e,c],d):b.api.addEventListener(e,c),b.P[e]=!0);else if(c=c.origin,b.api.isExternalMethodAvailable(e,c)){d=d||[];if(d.length>0&&bz(e)){var f=d;if(Sa(f[0])&&!Array.isArray(f[0]))var g=f[0];else switch(g={},e){case "loadVideoById":case "cueVideoById":g=cz(f[0],f[1]!==void 0?Number(f[1]):void 0,f[2]);break;case "loadVideoByUrl":case "cueVideoByUrl":g=f[0];typeof g==="string"&&(g={mediaContentUrl:g,
startSeconds:f[1]!==void 0?Number(f[1]):void 0,suggestedQuality:f[2]});c:{if((f=g.mediaContentUrl)&&(f=/\/([ve]|embed)\/([^#?]+)/.exec(f))&&f[2]){f=f[2];break c}f=null}g.videoId=f;g=cz(g);break;case "loadPlaylist":case "cuePlaylist":g=dz(f[0],f[1],f[2],f[3])}d.length=1;d[0]=g}b.api.handleExternalCall(e,d,c);bz(e)&&iz(b,fz(b))}}}};
jz.addEventListener("message",this.G);if(a=P("WIDGET_ID"))this.sessionId=a;kz(this,"onReady",function(){b.u=!0;var c=b.api.getVideoData();if(!c.isPlayable){b.Z=!0;c=c.errorCode;var d=d===void 0?5:d;b.errorCode=c?$y[c]||d:d;b.sendMessage("onError",Number(b.errorCode))}});
kz(this,"onVideoProgress",this.lf.bind(this));kz(this,"onVolumeChange",this.mf.bind(this));kz(this,"onApiChange",this.df.bind(this));kz(this,"onPlaybackQualityChange",this.hf.bind(this));kz(this,"onPlaybackRateChange",this.jf.bind(this));kz(this,"onStateChange",this.kf.bind(this));kz(this,"onWebglSettingsChanged",this.nf.bind(this));kz(this,"onCaptionsTrackListChanged",this.ef.bind(this));kz(this,"captionssettingschanged",this.ff.bind(this))}
w(ez,F);function iz(a,b){a.sendMessage("infoDelivery",b)}
r=ez.prototype;r.sendMessage=function(a,b){a={event:a,info:b===void 0?null:b};this.Y?gz(this,a):this.D.push(a)};
function hz(a,b,c){return function(d){b==="onError"?a.api.logApiCall(b+" invocation",c,d):a.api.logApiCall(b+" invocation",c);a.sendMessage(b,d)}}
function kz(a,b,c){a.j.push({eventType:b,listener:c});a.api.addEventListener(b,c)}
function fz(a){if(!a.api)return null;var b=a.api.getApiInterface();Xb(b,"getVideoData");for(var c={apiInterface:b},d=0,e=b.length;d<e;d++){var f=b[d];if(f.search("get")===0||f.search("is")===0){var g=0;f.search("get")===0?g=3:f.search("is")===0&&(g=2);g=f.charAt(g).toLowerCase()+f.substring(g+1);try{var h=a.api[f]();c[g]=h}catch(k){}}}c.videoData=a.api.getVideoData();c.currentTimeLastUpdated_=Date.now()/1E3;return c}
r.kf=function(a){a={playerState:a,currentTime:this.api.getCurrentTime(),duration:this.api.getDuration(),videoData:this.api.getVideoData(),videoStartBytes:0,videoBytesTotal:this.api.getVideoBytesTotal(),videoLoadedFraction:this.api.getVideoLoadedFraction(),playbackQuality:this.api.getPlaybackQuality(),availableQualityLevels:this.api.getAvailableQualityLevels(),currentTimeLastUpdated_:Date.now()/1E3,playbackRate:this.api.getPlaybackRate(),mediaReferenceTime:this.api.getMediaReferenceTime()};this.api.getVideoUrl&&
(a.videoUrl=this.api.getVideoUrl());this.api.getVideoContentRect&&(a.videoContentRect=this.api.getVideoContentRect());this.api.getProgressState&&(a.progressState=this.api.getProgressState());this.api.getPlaylist&&(a.playlist=this.api.getPlaylist());this.api.getPlaylistIndex&&(a.playlistIndex=this.api.getPlaylistIndex());this.api.getStoryboardFormat&&(a.storyboardFormat=this.api.getStoryboardFormat());iz(this,a)};
r.hf=function(a){a={playbackQuality:a};this.api.getAvailableQualityLevels&&(a.availableQualityLevels=this.api.getAvailableQualityLevels());this.api.getPreferredQuality&&(a.preferredQuality=this.api.getPreferredQuality());iz(this,a)};
r.jf=function(a){iz(this,{playbackRate:a})};
r.df=function(){for(var a=this.api.getOptions(),b={namespaces:a},c=0,d=a.length;c<d;c++){var e=a[c],f=this.api.getOptions(e);a.join(", ");b[e]={options:f};for(var g=0,h=f.length;g<h;g++){var k=f[g],l=this.api.getOption(e,k);b[e][k]=l}}this.sendMessage("apiInfoDelivery",b)};
r.mf=function(){iz(this,{muted:this.api.isMuted(),volume:this.api.getVolume()})};
r.lf=function(a){a={currentTime:a,videoBytesLoaded:this.api.getVideoBytesLoaded(),videoLoadedFraction:this.api.getVideoLoadedFraction(),currentTimeLastUpdated_:Date.now()/1E3,playbackRate:this.api.getPlaybackRate(),mediaReferenceTime:this.api.getMediaReferenceTime()};this.api.getProgressState&&(a.progressState=this.api.getProgressState());iz(this,a)};
r.nf=function(){iz(this,{sphericalProperties:this.api.getSphericalProperties()})};
r.ef=function(){if(this.api.getCaptionTracks){var a={captionTracks:this.api.getCaptionTracks()};iz(this,a)}};
r.ff=function(){if(this.api.getSubtitlesUserSettings){var a={subtitlesUserSettings:this.api.getSubtitlesUserSettings()};iz(this,a)}};
function gz(a,b){if(a.h){b.channel="widget";a.sessionId&&(b.id=a.sessionId);try{var c=JSON.stringify(b);a.h.postMessage(c,a.targetOrigin)}catch(d){V(d)}}}
r.ba=function(){F.prototype.ba.call(this);jz.removeEventListener("message",this.G);for(var a=0;a<this.j.length;a++){var b=this.j[a];this.api.removeEventListener(b.eventType,b.listener)}this.j=[];for(a=0;a<this.i.length;a++)b=this.i[a],this.U?this.api.handleExternalCall("removeEventListener",[b.eventType,b.listener],b.origin):this.api.removeEventListener(b.eventType,b.listener);this.i=[]};
var jz=window;function lz(a,b,c){F.call(this);var d=this;this.api=a;this.id=b;this.origin=c;this.h={};this.j=R("web_player_split_event_bus_iframe");this.i=function(e){a:if(e.origin===d.origin){var f=e.data;if(typeof f==="string"){try{f=JSON.parse(f)}catch(k){break a}if(f.command){var g=f.command;f=f.data;e=e.origin;if(!d.ea){var h=f||{};switch(g){case "addEventListener":typeof h.event==="string"&&d.addListener(h.event,e);break;case "removeEventListener":typeof h.event==="string"&&d.removeListener(h.event,e);break;
default:d.api.isReady()&&d.api.isExternalMethodAvailable(g,e||null)&&(f=mz(g,f||{}),f=d.api.handleExternalCall(g,f,e||null),(f=nz(g,f))&&oz(d,g,f))}}}}}};
pz.addEventListener("message",this.i);oz(this,"RECEIVING")}
w(lz,F);r=lz.prototype;r.addListener=function(a,b){if(!(a in this.h)){var c=this.gf.bind(this,a);this.h[a]=c;this.addEventListener(a,c,b)}};
r.gf=function(a,b){this.ea||oz(this,a,qz(a,b))};
r.removeListener=function(a,b){a in this.h&&(this.removeEventListener(a,this.h[a],b),delete this.h[a])};
r.addEventListener=function(a,b,c){this.j?a==="onReady"?this.api.addEventListener(a,b):this.api.handleExternalCall("addEventListener",[a,b],c||null):this.api.addEventListener(a,b)};
r.removeEventListener=function(a,b,c){this.j?a==="onReady"?this.api.removeEventListener(a,b):this.api.handleExternalCall("removeEventListener",[a,b],c||null):this.api.removeEventListener(a,b)};
function mz(a,b){switch(a){case "loadVideoById":return[cz(b)];case "cueVideoById":return[cz(b)];case "loadVideoByPlayerVars":return[b];case "cueVideoByPlayerVars":return[b];case "loadPlaylist":return[dz(b)];case "cuePlaylist":return[dz(b)];case "seekTo":return[b.seconds,b.allowSeekAhead];case "playVideoAt":return[b.index];case "setVolume":return[b.volume];case "setPlaybackQuality":return[b.suggestedQuality];case "setPlaybackRate":return[b.suggestedRate];case "setLoop":return[b.loopPlaylists];case "setShuffle":return[b.shufflePlaylist];
case "getOptions":return[b.module];case "getOption":return[b.module,b.option];case "setOption":return[b.module,b.option,b.value];case "handleGlobalKeyDown":return[b.keyCode,b.shiftKey,b.ctrlKey,b.altKey,b.metaKey,b.key,b.code]}return[]}
function nz(a,b){switch(a){case "isMuted":return{muted:b};case "getVolume":return{volume:b};case "getPlaybackRate":return{playbackRate:b};case "getAvailablePlaybackRates":return{availablePlaybackRates:b};case "getVideoLoadedFraction":return{videoLoadedFraction:b};case "getPlayerState":return{playerState:b};case "getCurrentTime":return{currentTime:b};case "getPlaybackQuality":return{playbackQuality:b};case "getAvailableQualityLevels":return{availableQualityLevels:b};case "getDuration":return{duration:b};
case "getVideoUrl":return{videoUrl:b};case "getVideoEmbedCode":return{videoEmbedCode:b};case "getPlaylist":return{playlist:b};case "getPlaylistIndex":return{playlistIndex:b};case "getOptions":return{options:b};case "getOption":return{option:b}}}
function qz(a,b){switch(a){case "onReady":return;case "onStateChange":return{playerState:b};case "onPlaybackQualityChange":return{playbackQuality:b};case "onPlaybackRateChange":return{playbackRate:b};case "onError":return{errorCode:b}}if(b!=null)return{value:b}}
function oz(a,b,c){a.ea||(b={id:a.id,command:b},c&&(b.data=c),rz.postMessage(JSON.stringify(b),a.origin))}
r.ba=function(){pz.removeEventListener("message",this.i);for(var a in this.h)this.h.hasOwnProperty(a)&&this.removeListener(a);F.prototype.ba.call(this)};
var pz=window,rz=window.parent;var sz=new My;function tz(){return sz.jd()}
function uz(a){a=a===void 0?{}:a;return sz.invoke(a)}
;function vz(a,b,c,d,e){F.call(this);var f=this;this.D=b;this.webPlayerContextConfig=d;this.Ec=e;this.Pa=!1;this.api={};this.pa=this.u=null;this.U=new M;this.h={};this.Z=this.xa=this.elementId=this.Qa=this.config=null;this.Y=!1;this.j=this.G=null;this.Fa={};this.Fc=["onReady"];this.lastError=null;this.rb=NaN;this.P={};this.ha=0;this.i=this.o=a;Bc(this,this.U);wz(this);c?this.ha=setTimeout(function(){f.loadNewVideoConfig(c)},0):d&&(xz(this),yz(this))}
w(vz,F);r=vz.prototype;r.getId=function(){return this.D};
r.loadNewVideoConfig=function(a){if(!this.ea){this.ha&&(clearTimeout(this.ha),this.ha=0);var b=a||{};b instanceof ov||(b=new ov(b));this.config=b;this.setConfig(a);yz(this);this.isReady()&&zz(this)}};
function xz(a){var b;a.webPlayerContextConfig?b=a.webPlayerContextConfig.rootElementId:b=a.config.attrs.id;a.elementId=b||a.elementId;a.elementId==="video-player"&&(a.elementId=a.D,a.webPlayerContextConfig?a.webPlayerContextConfig.rootElementId=a.D:a.config.attrs.id=a.D);var c;((c=a.i)==null?void 0:c.id)===a.elementId&&(a.elementId+="-player",a.webPlayerContextConfig?a.webPlayerContextConfig.rootElementId=a.elementId:a.config.attrs.id=a.elementId)}
r.setConfig=function(a){this.Qa=a;this.config=Az(a);xz(this);if(!this.xa){var b;this.xa=Bz(this,((b=this.config.args)==null?void 0:b.jsapicallback)||"onYouTubePlayerReady")}this.config.args?this.config.args.jsapicallback=null:this.config.args={jsapicallback:null};var c;if((c=this.config)==null?0:c.attrs)a=this.config.attrs,(b=a.width)&&this.i&&(this.i.style.width=Uj(Number(b)||b)),(a=a.height)&&this.i&&(this.i.style.height=Uj(Number(a)||a))};
function zz(a){if(a.config&&a.config.loaded!==!0)if(a.config.loaded=!0,!a.config.args||a.config.args.autoplay!=="0"&&a.config.args.autoplay!==0&&a.config.args.autoplay!==!1){var b;a.api.loadVideoByPlayerVars((b=a.config.args)!=null?b:null)}else a.api.cueVideoByPlayerVars(a.config.args)}
function Cz(a){var b=!0,c=Dz(a);c&&a.config&&(b=c.dataset.version===Ez(a));return b&&!!E("yt.player.Application.create")}
function yz(a){if(!a.ea&&!a.Y){var b=Cz(a);if(b&&(Dz(a)?"html5":null)==="html5")a.Z="html5",a.isReady()||Fz(a);else if(Gz(a),a.Z="html5",b&&a.j&&a.o)a.o.appendChild(a.j),Fz(a);else{a.config&&(a.config.loaded=!0);var c=!1;a.G=function(){c=!0;var d=Hz(a,"player_bootstrap_method")?E("yt.player.Application.createAlternate")||E("yt.player.Application.create"):E("yt.player.Application.create");var e=a.config?Az(a.config):void 0;d&&d(a.o,e,a.webPlayerContextConfig,a.Ec);Fz(a)};
a.Y=!0;b?a.G():(zv(Ez(a),a.G),(b=Iz(a))&&Gv(b||""),Jz(a)&&!c&&D("yt.player.Application.create",null))}}}
function Dz(a){var b=Gg(a.elementId);!b&&a.i&&a.i.querySelector&&(b=a.i.querySelector("#"+a.elementId));return b}
function Fz(a){if(!a.ea){var b=Dz(a),c=!1;b&&b.getApiInterface&&b.getApiInterface()&&(c=!0);if(c){a.Y=!1;if(!Hz(a,"html5_remove_not_servable_check_killswitch")){var d;if((b==null?0:b.isNotServable)&&a.config&&(b==null?0:b.isNotServable((d=a.config.args)==null?void 0:d.video_id)))return}Kz(a)}else a.rb=setTimeout(function(){Fz(a)},50)}}
function Kz(a){wz(a);a.Pa=!0;var b=Dz(a);if(b){a.u=Lz(a,b,"addEventListener");a.pa=Lz(a,b,"removeEventListener");var c=b.getApiInterface();c=c.concat(b.getInternalApiInterface());for(var d=a.api,e=0;e<c.length;e++){var f=c[e];d[f]||(d[f]=Lz(a,b,f))}}for(var g in a.h)a.h.hasOwnProperty(g)&&a.u&&a.u(g,a.h[g]);zz(a);a.xa&&a.xa(a.api);a.U.qb("onReady",a.api)}
function Lz(a,b,c){var d=b[c];return function(){var e=B.apply(0,arguments);try{return a.lastError=null,d.apply(b,e)}catch(f){if(c!=="sendAbandonmentPing")throw f.params=c,a.lastError=f,e=new T("PlayerProxy error in method call",{error:f,method:c,playerId:a.D}),e.level="WARNING",e;}}}
function wz(a){a.Pa=!1;if(a.pa)for(var b in a.h)a.h.hasOwnProperty(b)&&a.pa(b,a.h[b]);for(var c in a.P)a.P.hasOwnProperty(c)&&clearTimeout(Number(c));a.P={};a.u=null;a.pa=null;b=a.api;for(var d in b)b.hasOwnProperty(d)&&(b[d]=null);b.addEventListener=function(e,f){a.addEventListener(e,f)};
b.removeEventListener=function(e,f){a.removeEventListener(e,f)};
b.destroy=function(){a.dispose()};
b.getLastError=function(){return a.getLastError()};
b.getPlayerType=function(){return a.getPlayerType()};
b.getCurrentVideoConfig=function(){return a.Qa};
b.loadNewVideoConfig=function(e){a.loadNewVideoConfig(e)};
b.isReady=function(){return a.isReady()}}
r.isReady=function(){return this.Pa};
r.addEventListener=function(a,b){var c=this,d=Bz(this,b);d&&(Rb(this.Fc,a)>=0||this.h[a]||(b=Mz(this,a),this.u&&this.u(a,b)),this.U.subscribe(a,d),a==="onReady"&&this.isReady()&&setTimeout(function(){d(c.api)},0))};
r.removeEventListener=function(a,b){this.ea||(b=Bz(this,b))&&this.U.unsubscribe(a,b)};
function Bz(a,b){var c=b;if(typeof b==="string"){if(a.Fa[b])return a.Fa[b];c=function(){var d=B.apply(0,arguments),e=E(b);if(e)try{e.apply(C,d)}catch(f){throw d=new T("PlayerProxy error when executing callback",{error:f}),d.level="ERROR",d;}};
a.Fa[b]=c}return c?c:null}
function Mz(a,b){function c(d){function e(){if(!a.ea)try{a.U.qb(b,d!=null?d:void 0)}catch(h){var g=new T("PlayerProxy error when creating global callback",{error:h.message,event:b,playerId:a.D,data:d,originalStack:h.stack,componentStack:h.he});g.level="WARNING";throw g;}}
if(Hz(a,"web_player_publish_events_immediately"))e();else{var f=setTimeout(function(){e();var g=a.P,h=String(f);h in g&&delete g[h]},0);
xg(a.P,String(f))}}
return a.h[b]=c}
r.getPlayerType=function(){return this.Z||(Dz(this)?"html5":null)};
r.getLastError=function(){return this.lastError};
function Gz(a){a.cancel();wz(a);a.Z=null;a.config&&(a.config.loaded=!1);var b=Dz(a);b&&(Cz(a)||!Jz(a)?a.j=b:(b&&b.destroy&&b.destroy(),a.j=null));if(a.o)for(a=a.o;b=a.firstChild;)a.removeChild(b)}
r.cancel=function(){this.G&&Dv(Ez(this),this.G);clearTimeout(this.rb);this.Y=!1};
r.ba=function(){Gz(this);if(this.j&&this.config&&this.j.destroy)try{this.j.destroy()}catch(b){var a=new T("PlayerProxy error during disposal",{error:b});a.level="ERROR";throw a;}this.Fa=null;for(a in this.h)this.h.hasOwnProperty(a)&&delete this.h[a];this.Qa=this.config=this.api=null;delete this.o;delete this.i;F.prototype.ba.call(this)};
function Jz(a){var b,c;a=(b=a.config)==null?void 0:(c=b.args)==null?void 0:c.fflags;return!!a&&a.indexOf("player_destroy_old_version=true")!==-1}
function Ez(a){return a.webPlayerContextConfig?a.webPlayerContextConfig.jsUrl:(a=a.config.assets)?a.js:""}
function Iz(a){return a.webPlayerContextConfig?a.webPlayerContextConfig.cssUrl:(a=a.config.assets)?a.css:""}
function Hz(a,b){if(a.webPlayerContextConfig)var c=a.webPlayerContextConfig.serializedExperimentFlags;else{var d;if((d=a.config)==null?0:d.args)c=a.config.args.fflags}return(c||"").split("&").includes(b+"=true")}
function Az(a){for(var b={},c=y(Object.keys(a)),d=c.next();!d.done;d=c.next()){d=d.value;var e=a[d];b[d]=typeof e==="object"?Ag(e):e}return b}
;var Nz={},Oz="player_uid_"+(Math.random()*1E9>>>0);function Pz(a,b){var c="player",d=!1;d=d===void 0?!0:d;c=typeof c==="string"?Gg(c):c;var e=Oz+"_"+Ta(c),f=Nz[e];if(f&&d)return Qz(a,b)?f.api.loadVideoByPlayerVars(a.args||null):f.loadNewVideoConfig(a),f.api;f=new vz(c,e,a,b,void 0);Nz[e]=f;f.addOnDisposeCallback(function(){delete Nz[f.getId()]});
return f.api}
function Qz(a,b){return b&&b.serializedExperimentFlags?b.serializedExperimentFlags.includes("web_player_remove_playerproxy=true"):a&&a.args&&a.args.fflags?a.args.fflags.includes("web_player_remove_playerproxy=true"):!1}
;var Rz=null,Sz=null;
function Tz(){xx();var a=Hn(),b=Kn(119),c=window.devicePixelRatio>1;if(document.body&&ik(document.body,"exp-invert-logo"))if(c&&!ik(document.body,"inverted-hdpi")){var d=document.body;if(d.classList)d.classList.add("inverted-hdpi");else if(!ik(d,"inverted-hdpi")){var e=gk(d);hk(d,e+(e.length>0?" inverted-hdpi":"inverted-hdpi"))}}else!c&&ik(document.body,"inverted-hdpi")&&jk();if(b!=c){b="f"+(Math.floor(119/31)+1);d=Ln(b)||0;d=c?d|67108864:d&-67108865;d===0?delete En[b]:(c=d.toString(16),En[b]=c.toString());
c=!0;R("web_secure_pref_cookie_killswitch")&&(c=!1);b=a.h;d=[];for(f in En)En.hasOwnProperty(f)&&d.push(f+"="+encodeURIComponent(String(En[f])));var f=d.join("&");An(b,f,63072E3,a.i,c)}}
function Uz(){Vz()}
function Wz(){sx("ep_init_pr");Vz()}
function Vz(){var a=Rz.getVideoData(1);a=a.title?a.title+" - YouTube":"YouTube";document.title!==a&&(document.title=a)}
function Xz(){Rz&&Rz.sendAbandonmentPing&&Rz.sendAbandonmentPing();P("PL_ATT")&&sz.dispose();for(var a=ak,b=0,c=fy.length;b<c;b++)a.qa(fy[b]);fy.length=0;Bv("//static.doubleclick.net/instream/ad_status.js");gy=!1;qm("DCLKSTAT",0);Ac(Sz);Rz&&(Rz.removeEventListener("onVideoDataChange",Uz),Rz.destroy())}
;sx("ep_init_eps");D("yt.setConfig",qm);D("yt.config.set",qm);D("yt.setMsg",yv);D("yt.msgs.set",yv);D("yt.logging.errors.log",wu);
D("writeEmbed",function(){sx("ep_init_wes");var a=P("PLAYER_CONFIG");if(!a){var b=P("PLAYER_VARS");b&&(a={args:b})}jw(!0);a.args.ps==="gvn"&&(document.body.style.backgroundColor="transparent");a.attrs||(a.attrs={width:"100%",height:"100%",id:"video-player"});var c=document.referrer;b=P("POST_MESSAGE_ORIGIN");window!==window.top&&c&&c!==document.URL&&(a.args.loaderUrl=c);ox("embed",["ol"]);c=vy();if(!c.serializedForcedExperimentIds){var d=Fm(window.location.href);d.forced_experiments&&(c.serializedForcedExperimentIds=
d.forced_experiments)}var e;((e=a.args)==null?0:e.autoplay)&&ox("watch",["pbs","pbu","pbp"]);Rz=Pz(a,c);Rz.addEventListener("onVideoDataChange",Uz);Rz.addEventListener("onReady",Wz);a=P("POST_MESSAGE_ID","player");P("ENABLE_JS_API")?Sz=new ez(Rz):P("ENABLE_POST_API")&&typeof a==="string"&&typeof b==="string"&&(Sz=new lz(Rz,a,b));hy();R("ytidb_create_logger_embed_killswitch")||Go();a={};Sy.h||(Sy.h=new Sy);Sy.h.install((a.flush_logs={callback:function(){bu()}},a));
ss();R("ytidb_clear_embedded_player")&&ak.ma(function(){ay();Hy()});
R("enable_rta_manager")&&ak.ma(function(){var f=new ty;var g=R("enable_rta_nsm")?new es:void 0;ky.h=new ky(f,g);f=ky.h;g=f.i.bind(f);D("yt.aba.att",g);f=f.j.bind(f);D("yt.aba.att2",f)});
sx("ep_init_wee")});
D("yt.abuse.player.botguardInitialized",E("yt.abuse.player.botguardInitialized")||tz);D("yt.abuse.player.invokeBotguard",E("yt.abuse.player.invokeBotguard")||uz);D("yt.abuse.dclkstatus.checkDclkStatus",E("yt.abuse.dclkstatus.checkDclkStatus")||iy);D("yt.player.exports.navigate",E("yt.player.exports.navigate")||iw);D("yt.util.activity.init",E("yt.util.activity.init")||Ks);D("yt.util.activity.getTimeSinceActive",E("yt.util.activity.getTimeSinceActive")||Ns);
D("yt.util.activity.setTimestamp",E("yt.util.activity.setTimestamp")||Ls);window.addEventListener("load",um(function(){Tz()}));
window.addEventListener("pageshow",um(function(a){a.persisted||Tz()}));
window.addEventListener("pagehide",um(function(a){R("embeds_web_enable_dispose_player_if_page_not_cached_killswitch")?Xz():a.persisted||Xz()}));
window.onerror=function(a,b,c,d,e){var f;b=b===void 0?"Unknown file":b;c=c===void 0?0:c;var g=!1,h=rm("log_window_onerror_fraction");if(h&&Math.random()<h)g=!0;else{h=document.getElementsByTagName("script");for(var k=0,l=h.length;k<l;k++)if(h[k].src.indexOf("/debug-")>0){g=!0;break}}if(g){g=!1;e?g=!0:(typeof a==="string"?h=a:ErrorEvent&&a instanceof ErrorEvent?(g=!0,h=a.message,b=a.filename,c=a.lineno,d=a.colno):(h="Unknown error",b="Unknown file",c=0),e=new T(h),e.name="UnhandledWindowError",e.message=
h,e.fileName=b,e.lineNumber=c,isNaN(d)?delete e.columnNumber:e.columnNumber=d);if(!R("wiz_enable_component_stack_propagation_killswitch")){a=e;var m;if((m=f)==null||!m.componentStack)if(m=a.he)f||(f={}),f.componentStack=pu(m)}f&&zu(e,f);g?wu(e):V(e)}};
Li=xu;window.addEventListener("unhandledrejection",function(a){xu(a.reason)});
Sb(P("ERRORS")||[],function(a){wu.apply(null,a)});
qm("ERRORS",[]);sx("ep_init_epe");}).call(this);
