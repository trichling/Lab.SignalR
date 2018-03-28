import Vue from "vue";
import { Gauge } from "gaugeJS";

Vue.component('gauge-js', {
    props: ["value", "canvasId"],
    data: function() {
        return {
            gaugeControl: Gauge
        };
    },
    template: "<canvas :id='canvasId'></canvas>",
    watch: { 
        value: function(newVal, oldVal) { this.gaugeControl.set(newVal); }
    },
    mounted () {
      var opts = {
        angle: 0.15, /// The span of the gauge arc
        lineWidth: 0.44, // The line thickness
        pointer: {
          length: 0.9, // Relative to gauge radius
          strokeWidth: 0.035 // The thickness
        },
        colorStart: '#6FADCF',   // Colors
        colorStop: '#8FC0DA',    // just experiment with them
        strokeColor: '#E0E0E0'   // to see which ones work best for you
      };
      var target = document.getElementById(this.canvasId); // your canvas element
      this.gaugeControl = new Gauge(target).setOptions(opts); // create sexy gauge!
      this.gaugeControl.maxValue = 10; // set max gauge value
      this.gaugeControl.setMinValue(0);  // set min value
      this.gaugeControl.set(this.value); // set actual value
   }
})