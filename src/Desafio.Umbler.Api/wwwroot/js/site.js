/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/*!***********************!*\
  !*** ./src/js/app.js ***!
  \***********************/
/*! dynamic exports provided */
/*! all exports used */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\n\nvar _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nvar Request = window.Request;\nvar Headers = window.Headers;\nvar fetch = window.fetch;\n\nvar Api = function () {\n  function Api() {\n    _classCallCheck(this, Api);\n  }\n\n  _createClass(Api, [{\n    key: 'request',\n    value: async function request(method, url, body) {\n      if (body) {\n        body = JSON.stringify(body);\n      }\n\n      var request = new Request('/api/' + url, {\n        method: method,\n        body: body,\n        credentials: 'same-origin',\n        headers: new Headers({\n          'Accept': 'application/json',\n          'Content-Type': 'application/json'\n        })\n      });\n\n      var resp = await fetch(request);\n      if (!resp.ok && resp.status !== 400) {\n        throw Error(resp.statusText);\n      }\n\n      var jsonResult = await resp.json();\n\n      if (resp.status === 400) {\n        jsonResult.requestStatus = 400;\n      }\n\n      return jsonResult;\n    }\n  }, {\n    key: 'getDomain',\n    value: async function getDomain(domainOrIp) {\n      return this.request('GET', 'domain/' + domainOrIp);\n    }\n  }]);\n\n  return Api;\n}();\n\nvar api = new Api();\n\nvar callback = function callback() {\n  var btn = document.getElementById('btn-search');\n  var txt = document.getElementById('txt-search');\n  var result = document.getElementById('whois-results');\n\n  if (btn) {\n    btn.onclick = async function () {\n      var response = await api.getDomain(txt.value);\n      if (response) {\n        result.innerHTML = JSON.stringify(response, null, 4);\n      }\n    };\n  }\n};\n\nif (document.readyState === 'complete' || document.readyState !== 'loading' && !document.documentElement.doScroll) {\n  callback();\n} else {\n  document.addEventListener('DOMContentLoaded', callback);\n}//# sourceURL=[module]\n//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiMC5qcyIsInNvdXJjZXMiOlsid2VicGFjazovLy9zcmMvanMvYXBwLmpzPzcxNmYiXSwic291cmNlc0NvbnRlbnQiOlsiY29uc3QgUmVxdWVzdCA9IHdpbmRvdy5SZXF1ZXN0XHJcbmNvbnN0IEhlYWRlcnMgPSB3aW5kb3cuSGVhZGVyc1xyXG5jb25zdCBmZXRjaCA9IHdpbmRvdy5mZXRjaFxyXG5cclxuY2xhc3MgQXBpIHtcclxuICBhc3luYyByZXF1ZXN0IChtZXRob2QsIHVybCwgYm9keSkge1xyXG4gICAgaWYgKGJvZHkpIHtcclxuICAgICAgYm9keSA9IEpTT04uc3RyaW5naWZ5KGJvZHkpXHJcbiAgICB9XHJcblxyXG4gICAgY29uc3QgcmVxdWVzdCA9IG5ldyBSZXF1ZXN0KCcvYXBpLycgKyB1cmwsIHtcclxuICAgICAgbWV0aG9kOiBtZXRob2QsXHJcbiAgICAgIGJvZHk6IGJvZHksXHJcbiAgICAgIGNyZWRlbnRpYWxzOiAnc2FtZS1vcmlnaW4nLFxyXG4gICAgICBoZWFkZXJzOiBuZXcgSGVhZGVycyh7XHJcbiAgICAgICAgJ0FjY2VwdCc6ICdhcHBsaWNhdGlvbi9qc29uJyxcclxuICAgICAgICAnQ29udGVudC1UeXBlJzogJ2FwcGxpY2F0aW9uL2pzb24nXHJcbiAgICAgIH0pXHJcbiAgICB9KVxyXG5cclxuICAgIGNvbnN0IHJlc3AgPSBhd2FpdCBmZXRjaChyZXF1ZXN0KVxyXG4gICAgaWYgKCFyZXNwLm9rICYmIHJlc3Auc3RhdHVzICE9PSA0MDApIHtcclxuICAgICAgdGhyb3cgRXJyb3IocmVzcC5zdGF0dXNUZXh0KVxyXG4gICAgfVxyXG5cclxuICAgIGNvbnN0IGpzb25SZXN1bHQgPSBhd2FpdCByZXNwLmpzb24oKVxyXG5cclxuICAgIGlmIChyZXNwLnN0YXR1cyA9PT0gNDAwKSB7XHJcbiAgICAgIGpzb25SZXN1bHQucmVxdWVzdFN0YXR1cyA9IDQwMFxyXG4gICAgfVxyXG5cclxuICAgIHJldHVybiBqc29uUmVzdWx0XHJcbiAgfVxyXG5cclxuICBhc3luYyBnZXREb21haW4gKGRvbWFpbk9ySXApIHtcclxuICAgIHJldHVybiB0aGlzLnJlcXVlc3QoJ0dFVCcsIGBkb21haW4vJHtkb21haW5PcklwfWApXHJcbiAgfVxyXG59XHJcblxyXG5jb25zdCBhcGkgPSBuZXcgQXBpKClcclxuXHJcbnZhciBjYWxsYmFjayA9ICgpID0+IHtcclxuICBjb25zdCBidG4gPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZCgnYnRuLXNlYXJjaCcpXHJcbiAgY29uc3QgdHh0ID0gZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoJ3R4dC1zZWFyY2gnKVxyXG4gIGNvbnN0IHJlc3VsdCA9IGRvY3VtZW50LmdldEVsZW1lbnRCeUlkKCd3aG9pcy1yZXN1bHRzJylcclxuXHJcbiAgaWYgKGJ0bikge1xyXG4gICAgYnRuLm9uY2xpY2sgPSBhc3luYyAoKSA9PiB7XHJcbiAgICAgIGNvbnN0IHJlc3BvbnNlID0gYXdhaXQgYXBpLmdldERvbWFpbih0eHQudmFsdWUpXHJcbiAgICAgIGlmIChyZXNwb25zZSkge1xyXG4gICAgICAgIHJlc3VsdC5pbm5lckhUTUwgPSBKU09OLnN0cmluZ2lmeShyZXNwb25zZSwgbnVsbCwgNClcclxuICAgICAgfVxyXG4gICAgfVxyXG4gIH1cclxufVxyXG5cclxuaWYgKGRvY3VtZW50LnJlYWR5U3RhdGUgPT09ICdjb21wbGV0ZScgfHwgKGRvY3VtZW50LnJlYWR5U3RhdGUgIT09ICdsb2FkaW5nJyAmJiAhZG9jdW1lbnQuZG9jdW1lbnRFbGVtZW50LmRvU2Nyb2xsKSkge1xyXG4gIGNhbGxiYWNrKClcclxufSBlbHNlIHtcclxuICBkb2N1bWVudC5hZGRFdmVudExpc3RlbmVyKCdET01Db250ZW50TG9hZGVkJywgY2FsbGJhY2spXHJcbn1cclxuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIHNyYy9qcy9hcHAuanMiXSwibWFwcGluZ3MiOiI7Ozs7OztBQUFBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7Ozs7Ozs7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFGQTtBQUpBO0FBQ0E7QUFTQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7O0FBRUE7QUFDQTtBQUNBOzs7Ozs7QUFHQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQSIsInNvdXJjZVJvb3QiOiIifQ==\n//# sourceURL=webpack-internal:///0\n");

/***/ })
/******/ ]);