// ------------------------------------------------------------------------------------------------------------------------
// Timeouts.
// ------------------------------------------------------------------------------------------------------------------------
// Variant 1.
wait: function (ms) {
    var start = new Date().getTime();
    var end = start;
    while (end < start + ms) {
        end = new Date().getTime();
    }
},
// Using wait.
this.wait(1000);
// ------------------------------------------------------------------------------------------------------------------------
// Variant 2.
// https://www.w3schools.com/js/js_timing.asp
let dt = new Date();
console.info("start: " + dt.getMinutes() + "." + dt.getSeconds() + "." + dt.getMilliseconds() + ".");
setTimeout(this.foo1, 1000);
setTimeout(this.foo.bind(this, "some"), 1000);
foo1() {
  console.info("foo1");
  let dt = new Date();
  console.info("foo1: " + dt.getMinutes() + "." + dt.getSeconds() + "." + dt.getMilliseconds() + ".");
},
foo2(args) {
  console.info("foo2(" + args + ")");
  let dt = new Date();
  console.info("foo2: " + dt.getMinutes() + "." + dt.getSeconds() + "." + dt.getMilliseconds() + ".");
},
// ------------------------------------------------------------------------------------------------------------------------
// Variant 3.
// https://academy.terrasoft.ru/docs/7-17/developer/front-end_development/modules/principy_modulnoy_razrabotki_v_creatio#title-2182-11
define("ModuleExample", [], function () {
    Ext.define("Terrasoft.configuration.ModuleExample", {
        alternateClassName: "Terrasoft.ModuleExample",
        Ext: null,
        sandbox: null,
        Terrasoft: null,
        // При инициализации модуля выполнится первым.
        init: function (callback) {
            setTimeout(callback, 2000);
        },
        render: function (renderTo) {
            // Метод выполнится c задержкой в 2 секудны,
            // Задержка указана в аргументе функции setTimeout() в методе init().
        }
    });
});
// ------------------------------------------------------------------------------------------------------------------------
