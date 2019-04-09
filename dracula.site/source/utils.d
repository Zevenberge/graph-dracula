module utils;

string js(string file)
{
    import std.format : format;
    return "script(src='%s.js')".format(file);
}