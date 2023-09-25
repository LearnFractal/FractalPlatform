if (typeof m == 'undefined') {
    var m = function () {};
    m.fn = m.prototype = {};
}

m.fn.class_toggle = function(context){
    return this.on('click', function(e) {
        if (typeof this.getAttribute !== 'function' || this.getAttribute('data-m-class') === null)
            return true;
        e.preventDefault();
        m(context).class(this.getAttribute('data-m-class'), ':toggle');
    });
};