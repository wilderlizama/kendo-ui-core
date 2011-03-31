(function ($) {
    var kendo = window.kendo,
        ui = kendo.ui = kendo.ui || {},
        DEFAULT_PRECISION = 6,
        ZERO_THRESHOLD = 0.2;

    function Chart(element, options) {
        this.options = $.extend(Chart.prototype.defaults, options);
        this.element = element;
    }

    Chart.prototype = {
        defaults: {

        },

        types: { }
    };

    ui.Chart = Chart;
    $.fn.kendoChart = function(options) {
        $(this).each(function() {
            $(this).data("kendoChart", new kendo.ui.Chart(this, options));
        });

        return this;
    };


    // Numeric Axis
    function NumericAxis() {

    }

    NumericAxis.prototype = {
        getMajorUnit: function (min, max) {
            var diff = max - min;
            if (diff == 0) {
                if (max == 0) {
                    return 0.1;
                }

                diff = Math.abs(max);
            }

            var scale = Math.pow(10, Math.floor(Math.log(diff) / Math.log(10))),
                relativeValue = round((diff / scale), DEFAULT_PRECISION),
                scaleMultiplier = 1;

            if (relativeValue < 1.904762) {
                scaleMultiplier = 0.2;
            } else if (relativeValue < 4.761904) {
                scaleMultiplier = 0.5;
            } else if (relativeValue < 9.523809) {
                scaleMultiplier = 1;
            } else {
                scaleMultiplier = 2;
            }

            return round(scale * scaleMultiplier, DEFAULT_PRECISION);
        },

        getAxisMax: function(min, max) {
            if (min == 0 && max == 0) {
                return 1;
            }

            var axisMax;
            if (min <= 0 && max <= 0) {
                max = min == max ? 0 : max;

                var diff = Math.abs((max - min) / max);
                if(diff > ZERO_THRESHOLD) {
                    return 0;
                }

                axisMax = max - ((min - max) / 2);
            } else {
                min = min == max ? 0 : min;
                axisMax = max + 0.05 * (max - min);
            }

            var mu = this.getMajorUnit(min, max);
            return ceil(axisMax, mu);
        },

        getAxisMin: function(min, max) {
            if (min == 0 && max == 0) {
                return 0;
            }

            var axisMin;
            if (min >= 0 && max >= 0) {
                min = min == max ? 0 : min;

                var diff = (max - min) / max;
                if(diff > ZERO_THRESHOLD) {
                    return 0;
                }

                axisMin = min - ((max - min) / 2);
            } else {
                max = min == max ? 0 : max;
                axisMin = min + 0.05 * (min - max);
            }

            var mu = this.getMajorUnit(min, max);
            return floor(axisMin, mu);
        },
    };


    function BlockElement() {
        kendo.core.Observable.call(this);
        this.parentNode = null;
        this.children = [];
    }

    $.extend(BlockElement.prototype, new kendo.core.Observable, {
        appendChild: function(child) {
            this.children.push(child);
            child.parentNode = this;
            return this;
        },

        end: function() {
            return this.parentNode;
        }
    });


    function Row() {
        BlockElement.call(this);
    }

    $.extend(Row.prototype, new BlockElement, {
        addCell: function(cellContent) {
            var cell = new Cell();
            this.appendChild(cell);

            if (cellContent) {
                cell.appendChild(cellContent);
            }

            return cell;
        }
    });


    function Cell() {
        BlockElement.call(this);
    }

    $.extend(Cell.prototype, new BlockElement, {
        colspan: function(value) {
            if (value) {
                this._colspan = value;
                return this;
            } else {
                return this._colspan;
            }
        },
    });


    function GridLayout(options) {
        BlockElement.call(this);
        this.options = options;
    }

    $.extend(GridLayout.prototype, new BlockElement, {
        addRow: function() {
            var row = new Row();
            this.appendChild(row);

            return row;
        },

        apply: function() {

        }
    });

    // Helper functions
    function supportsSVG() {
        return document.implementation.hasFeature(
            "http://www.w3.org/TR/SVG11/feature#BasicStructure", "1.1");
    }

    function ceil(value, step) {
        return round(Math.ceil(value / step) * step, DEFAULT_PRECISION);
    }

    function floor(value, step) {
        return round(Math.floor(value / step) * step, DEFAULT_PRECISION);
    }

    function round(value, precision) {
        var power = Math.pow(10, precision || 0);
        return Math.round(value * power) / power;
    }

    // #ifdef DEBUG
    // Make the internal functions public for unit testing

    Chart.NumericAxis = NumericAxis;
    Chart.BlockElement = BlockElement;
    Chart.GridLayout = GridLayout;

    // #endif

})(jQuery);

// kendo.chart.bar.js
(function($) {
    function BarChart() {
    }

    kendo.ui.Chart.prototype.types["bar"] = function(chart, configuration) {
        return new BarChart(chart, configuration);
    };
})(jQuery);
