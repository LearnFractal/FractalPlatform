if (typeof m == 'undefined') {
    var m = function () {};
    m.fn = m.prototype = {};
}

m.fn.cookie_toggle_class = function(context) {

    var
        cookie_name = this.data.cookie || 'active',
        class_name = this.data.class || 'active',
        cookie = cookie_name === null ? false : m.cookies(cookie_name);

    context.class(class_name, cookie && cookie === class_name ? true : null);

    this.on('click', function(e) {
        var c = m.cookies(cookie_name);
        m.cookies(cookie_name, c && c === class_name ? null : class_name);
        context.class(class_name, c && c === class_name ? null : true);
    });
};