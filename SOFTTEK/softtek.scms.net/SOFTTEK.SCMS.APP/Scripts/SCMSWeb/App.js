var AppSCMS = (function () {
    
    // Variables
    // *********************************************************
    var _token;
    var _deviceID;
    var _app;

    function init() {
        //Security
        var salt = 'UHJ1ZWJhIGRlIGNvZGlmaWNhY2nDs24=';
        var pass = 'Softtek_Renovation_SCSM_2015';
        AppSCMS.async(SecuritySCMS.init, function () { }, salt, pass);
    }

    // Functions
    // *********************************************************

    // Set the token value
    function setToken(value) {
        _token = value;
    }

    // Set Device ID
    function setDeviceID(value) {
        _deviceID = value;
    }

    // Set APP 
    function setApp(value) {
        _app = value;
    }

    // 
    function retrieveParameter(parameterName, callback, onError) {
        var apiPath = '/api/parameter/';
        var header = 'Authorization';
        var authorizationType = 'Basic';

        $.ajax({
            url: '/' + _app + apiPath + parameterName,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                if (callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
                if (onError) {
                    onError(data);
                }
                
            }
        });
    }

    function retrieveSAPPMMasterData(callback, onError) {
        var apiPath = '/api/masterdata/';
        var header = 'Authorization';
        var authorizationType = 'Basic';

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                if (callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
                if (onError) {
                    onError(data);
                }

            }
        });
    }

    //
    function retrieveAppMenu(callback, onError) {
        var apiPath = '/api/menu/';
        var header = 'Authorization';
        var authorizationType = 'Basic';

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                if (callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function retrieveEmployeeImageForEmployeeId(employee_id, callback, onError) {
        var apiPath = '/api/employee/';
        var header = 'Authorization';
        var authorizationType = 'Basic';

        $.ajax({
            url: '/' + _app + apiPath + employee_id + '/image/',
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            success: function (data) {
                if (callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function retrieveEmployeeInformationForToken(callback, onError) {
        var apiPath = '/api/employee';
        var header = 'Authorization';
        var authorizationType = 'Basic';

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                if (callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function retriveSRAWeekInformation(employee_id, callback, onError) {
        var apiPath = '/api/activity/';
        var header = 'Authorization';
        var authorizationType = 'Basic';

        $.ajax({
            url: '/' + _app + apiPath + employee_id,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                if (callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function registerActivity(activity, callback, onError) {
        var apiPath = '/api/activity/';
        var header = 'Authorization';
        var authorizationType = 'Basic'

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'POST',
            data: activity,
            success: function (data) {
                if (callback) {
                    callback(data);
                }
                console.log(data);
            },
            error: function (data) {

                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function registerPermitsAndAbsences(activity, callback, onError) {
        var apiPath = '/api/PermitsAndAbsences/';
        var header = 'Authorization';
        var authorizationType = 'Basic'

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'POST',
            data: activity,
            success: function (data) {
                if (callback) {
                    callback(data);
                }
                console.log(data);
            },
            error: function (data) {

                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function SearchPermitsAndAbsencesForUser(id, callback, onError) {
        var apiPath = '/api/PermitsAndAbsences/';
        var header = 'Authorization';
        var authorizationType = 'Basic'

        $.ajax({
            url: '/' + _app + apiPath + id,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                if (callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function SearchActividadesUser(id, callback, onError) {
        var apiPath = '/api/Activity/';
        var header = 'Authorization';
        var authorizationType = 'Basic'

        $.ajax({
            url: '/' + _app + apiPath + id,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                if (callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function setEmployeeImageForEmployeeId(employeeID, image, progress, callback, onError) {
        var apiPath = '/api/employee/';
        var header = 'Authorization';
        var authorizationType = 'Basic';

        $.ajax({
            url: '/' + _app + apiPath + employeeID + '/image',
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            data: image,
            success: function (data) {
                if (callback) {
                    callback(data);
                }
                console.log(data);
            },
            error: function (data) {
                
                console.log(data);
                if (onError) {
                    onError(data);
                }
            },
            xhr: progress
        });
    }

    function async(operation, callback, args) {
        var _args = Array.prototype.splice.call(arguments, 2);
        setTimeout(function () {
            operation.apply(null,_args);
            if (callback) {
                callback();
            }
        }, 0);
    };

    function parseToSecureObject(id) {
        var obj = {};
        $.map($(id + ' :input'), function (n, i) {
            obj[n.name] = SecuritySCMS.encrypt($(n).val());
        });
        return obj;
    }

    function parseToObject(id) {
        var obj = {};
        $.map($(id + ' :input'), function (n, i) {
            obj[n.name] = $(n).val();
        });
        return obj;
    }

    function encodeBase64(plain_text)
    {
        var wordArray = CryptoJS.enc.Utf8.parse(plain_text);
        var text = CryptoJS.enc.Base64.stringify(wordArray);
        return text;
    }
    
    //--------------------------------------------------------------------------------
    //Methods for FA and PM

    function searchPMAdvice(modelAdvice, callback, onError) {
        var apiPath = '/api/PM/Advice/';
        var header = 'Authorization';
        var authorizationType = 'Basic'

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            data: modelAdvice,
            success: function (data) {
                if (callback) {
                    callback(data);
                }
                console.log(data);
            },
            error: function (data) {

                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function selectWorkOrders(modelWorkOrder, callback, onError) {
        var apiPath = '/api/WorkOrderPm';
        var header = 'Authorization';
        var authorizationType = 'Basic'

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            data: modelWorkOrder,
            success: function (data) {
                if (callback) {
                    callback(data);
                }
                console.log(data);
            },
            error: function (data) {

                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    function registerPMAdvice(modelAdvice, callback, onError) {
        var apiPath = '/api/WorkOrderPm/1/WorkOrder';
        var header = 'Authorization';
        var authorizationType = 'Basic'

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'GET',
            //data: modelAdvice,
            success: function (data) {
                if (callback) {
                    callback(data);
                }
                console.log(data);
            },
            error: function (data) {

                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }

    /*function updatePMAdvice(modelAdviceFilter,modelAdviceUpdate, callback, onError) {
        var apiPath = '/api/PM/Advice/';
        var header = 'Authorization';
        var authorizationType = 'Basic'

        $.ajax({
            url: '/' + _app + apiPath,
            beforeSend: function (xhr) {
                xhr.setRequestHeader(header, authorizationType + ' ' + AppSCMS.encodeBase64(_deviceID + ':' + _token));
            },
            type: 'POST',
            data: { modelAdviceFilter  modelAdviceUpdate},
            success: function (data) {
                if (callback) {
                    callback(data);
                }
                console.log(data);
            },
            error: function (data) {

                console.log(data);
                if (onError) {
                    onError(data);
                }
            }
        });
    }*/

    //--------------------------------------------------------------------------------

    /// Prototype
    return {
        init: init,
        parseToSecureObject: parseToSecureObject,
        parseToObject: parseToObject,
        async: async,
        token: { get: _token, set: setToken },
        deviceID: { get: _deviceID, set: setDeviceID },
        app: { get: _app, set: setApp },
        retrieveParameter: retrieveParameter,
        retrieveSAPPMMasterData: retrieveSAPPMMasterData,
        retrieveAppMenu: retrieveAppMenu,
        retrieveEmployeeImageForEmployeeId: retrieveEmployeeImageForEmployeeId,
        retrieveEmployeeInformationForToken: retrieveEmployeeInformationForToken,
        retriveSRAWeekInformation: retriveSRAWeekInformation,
        registerActivity: registerActivity,
        registerPermitsAndAbsences : registerPermitsAndAbsences,
        setEmployeeImageForEmployeeId: setEmployeeImageForEmployeeId,
        encodeBase64: encodeBase64,
        SearchPermitsAndAbsencesForUser: SearchPermitsAndAbsencesForUser,
        SearchActividadesUser: SearchActividadesUser,
        searchPMAdvice: searchPMAdvice,
        registerPMAdvice: registerPMAdvice/*,
        updatePMAdvice: updatePMAdvice*/
        , selectWorkOrders: selectWorkOrders
    }
}());