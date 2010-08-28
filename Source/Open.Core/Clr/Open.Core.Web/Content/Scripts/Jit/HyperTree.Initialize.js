function insertHyperTree(control, injectInto) {
    var hyperTree = new $jit.Hypertree({

        //id of the visualization container
        injectInto: injectInto,
        duration: 300,

        //Change node and edge styles such as
        //color, width and dimensions.
        Node: {
            dim: 9,
            color: "#1b83dc"
        },
        Edge: {
            lineWidth: 2,
            color: "#7dc0c6"
        },

        onBeforeCompute: function (node) {
            control.onBeforeCompute(node);
        },

        //Attach event handlers and add text to the
        //labels. This method is only triggered on label
        //creation
        onCreateLabel: function (domElement, node) {
            domElement.innerHTML = node.name;
            $jit.util.addEvent(domElement, 'click', function () {
                hyperTree.onClick(node.id);
                control.onNodeClick(node);
            });
        },
        //Change node styles when labels are placed
        //or moved.
        onPlaceLabel: function (domElement, node) {
            var style = domElement.style;
            style.display = '';
            style.cursor = 'pointer';
            if (node._depth <= 1) {
                style.fontSize = '0.9em';
                style.color = '#003e7b';

            } else if (node._depth == 2) {
                style.fontSize = "0.7em";
                style.color = "#7b7b7b";

            } else {
                style.display = 'none';
            }

            var left = parseInt(style.left);
            var w = domElement.offsetWidth;
            style.left = (left - w / 2) + 'px';
        },

        onAfterCompute: function () {
            control.onAfterCompute();
        }
    });

    return hyperTree;
}


function addHyperTreeNodes(control, hyperTree, parameters) {
    hyperTree.op.sum(parameters.data, {
                type: parameters.type,
                duration: parameters.duration,
                onComplete: function () { control.onAddComplete(); }
            });
}
