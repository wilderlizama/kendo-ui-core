
package com.kendoui.taglib.autocomplete;

import com.kendoui.taglib.BaseTag;

import com.kendoui.taglib.AutoCompleteTag;




import javax.servlet.jsp.JspException;

@SuppressWarnings("serial")
public class AnimationTag extends BaseTag /* interfaces *//* interfaces */ {
    
    @Override
    public int doEndTag() throws JspException {
//>> doEndTag


        AutoCompleteTag parent = (AutoCompleteTag)findParentWithClass(AutoCompleteTag.class);


        parent.setAnimation(this);

//<< doEndTag

        return super.doEndTag();
    }

    @Override
    public void initialize() {
//>> initialize
//<< initialize

        super.initialize();
    }

    @Override
    public void destroy() {
//>> destroy
//<< destroy

        super.destroy();
    }

//>> Attributes

    public static String tagName() {
        return "autoComplete-animation";
    }

    public void setClose(AnimationCloseTag value) {
        setProperty("close", value);
    }

    public void setOpen(AnimationOpenTag value) {
        setProperty("open", value);
    }

//<< Attributes

}
