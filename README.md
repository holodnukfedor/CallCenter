# CallCenter

Данное веб приложение выполнялось как тестовое задание

# Задача 

Есть некая сущность – звонок. Основными свойствами данной сущности являются:

* Длительность
* Статус звонка (соединен, брошен, пропущен, ошибка)
* Моменты начала звонка, соединения, терминирования
* Списки участников разговора (номера, идентификаторы)
* Списки связанных звонков (к примеру – если в процессе работы со звонком некий участник совершил дополнительный звонок, без прекращения основного звонка)
Набор свойств в какой-то момент времени может быть дополнен.

Предложите структуру хранения данной информации, сформируйте требования к базе данных хранящей данную информацию. Реализуйте веб-приложение осуществляющий доступ к данной информации и позволяющее осуществлять выборку звонков по одному или нескольким указанным выше свойствам.

Бонусные задания:

* Документирование приложения. Обоснование выбранного решения ( если посчитаете нужным).

Структуру базы данных можно увидеть на картинке: ![Иллюстрация к проекту](https://github.com/holodnukfedor/CallCenter/blob/master/callCenterDbScheme.png)

Скрипты, которые ее создают по адресу [https://github.com/holodnukfedor/CallCenter/tree/master/SQLScripts](https://github.com/holodnukfedor/CallCenter/tree/master/SQLScripts)
Хотя скрипты не нужны для создания БД, поскольку используется подход Entity framework Code First, при котором также в проекте БД заполняется тестовыми данными

Также стоит упомянуть что данная структура не соответсвует 3 нормальной форме, то есть избыточна. Имеется в виду поле DurationSeconds, оно должно обозначать длительность звонка. 
Которая вычисляется как разница между TerminationTime (время окончания звонка) и ConnectionTime (время соединения).
Но оно необходимо поскольку в EF Linq то SQL нет возможность вычислять разницу между не целочисленными типами. SQL запрос выглядел бы datediff(seconds, ConnectionTime, TerminationTime).
Было принято решение сделать структуры избыточной для удобства сортировки и фильтрации по этому полю.
В проекте реализован гибкий фильтр с помощью DynamicLinq, который создать предикаты фильтрации на основе строк приходящих от клиента вроде {GroupOperator: "AND", FilterRules: [{PropertyName: "Value", "PropertyValue" : "Value", op: "eq"}, {PropertyName: "Value2", "PropertyValue" : "Value2", op: "lt"}]}.
Данный фильтр можно использовать повторно для любых других таблиц. Проблему с избыточным полем DurationSeconds можно было бы решить через чистый SQL, но возникли неудобство с поззагрузкой связанных сущностей и тогда фильтр по остальным полям пришлось бы писать вручную. 
Также можно было решить проблему через View, но там также неочевидный маппинг или написать свой репозиторий вместо EntityFramework. Но эти подходы заняли бы слишком много времени.

Также реализованы пагинация и сортировка, поскольку они не усложняют задачу.