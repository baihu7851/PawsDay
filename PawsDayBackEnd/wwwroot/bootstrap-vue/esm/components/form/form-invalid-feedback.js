import { Vue, mergeData } from '../../vue';
import { NAME_FORM_INVALID_FEEDBACK } from '../../constants/components';
import { PROP_TYPE_BOOLEAN, PROP_TYPE_STRING } from '../../constants/props';
import { makeProp, makePropsConfigurable } from '../../utils/props'; // --- Props ---

export var props = makePropsConfigurable({
  ariaLive: makeProp(PROP_TYPE_STRING),
  forceShow: makeProp(PROP_TYPE_BOOLEAN, false),
  id: makeProp(PROP_TYPE_STRING),
  role: makeProp(PROP_TYPE_STRING),
  // Tri-state prop: `true`, `false`, or `null`
  state: makeProp(PROP_TYPE_BOOLEAN, null),
  tag: makeProp(PROP_TYPE_STRING, 'div'),
  tooltip: makeProp(PROP_TYPE_BOOLEAN, false)
}, NAME_FORM_INVALID_FEEDBACK); // --- Main component ---
// @vue/component

export var BFormInvalidFeedback = /*#__PURE__*/Vue.extend({
  name: NAME_FORM_INVALID_FEEDBACK,
  functional: true,
  props: props,
  render: function render(h, _ref) {
    var props = _ref.props,
        data = _ref.data,
        children = _ref.children;
    var tooltip = props.tooltip,
        ariaLive = props.ariaLive;
    var show = props.forceShow === true || props.state === false;
    return h(props.tag, mergeData(data, {
      class: {
        'd-block': show,
        'invalid-feedback': !tooltip,
        'invalid-tooltip': tooltip
      },
      attrs: {
        id: props.id || null,
        role: props.role || null,
        'aria-live': ariaLive || null,
        'aria-atomic': ariaLive ? 'true' : null
      }
    }), children);
  }
});