# Лабораторная работа №1

## Цель: 
1. Изучить основные возможности языка Python для разработки программных систем с интерфейсом командной строки (CLI)
2. Разработать программную систему на языке Python согласно описанию предметной области
## Задача:
Разработать программную систему на языке Python. Модель милиции.

<em>
Предметная область: органы внутренних дел и поддержание общественного порядка.<br>
Важные сущности: полиция, полицейский, преступление, законы, следствие, общественная безопасность.<br>
Операции: операция расследования преступлений, операция обеспечения общественного порядка, операция взаимодействия с гражданами, операция профилактики преступлений, операция задержания правонарушителей.
</em>

## Сущности:
Были выделены следующие сущности:

Полиция - Police <br>
Смена - Duty <br>
Полицейский - Officer <br>
Детектив - Detective <br>
Патрульный - PatrolOfficer <br>
Событие - Event <br>
Вызов - Call <br>
Преступление - Crime <br>
Расследование - Investigation <br>
Общественная безопасность - PublicSecurity <br>
Закон - Law <br>

## Диаграмма состояний
Составлена диаграмма состояний программной системы:

![image](https://github.com/hinderss/PPOIS_LABS/assets/111983708/5aa1c561-534d-4ac4-ba2c-3271978dc257)


## Диаграмма классов
На основе выделенных сущностей и состояний созданы следующие классы:

![image](https://github.com/hinderss/PPOIS_LABS/assets/111983708/ea9642b1-893b-409f-8e89-65712fc885c8)


## Описание работы программы:
- Приветствие: При запуске игры игрок приветствуется и ему предлагается ввести название города, где находится его полицейский участок. <br>
Количество сотрудников: После ввода названия города игра сообщает количество полицейских, работающих в участке. <br>
Ввод имени: Игрок вводит свое имя, чтобы игра могла обращаться к нему по имени. <br>

![1](https://github.com/hinderss/PPOIS_LABS/assets/111983708/9ddef229-4f95-46ad-89ff-a8f65b6065d8)


- Начало смены: В начале смены игрок видит информацию о текущей смене и списке доступных офицеров. <br>
Назначение офицеров на патруль: Игрок должен назначить офицеров на патрулирование. <br>

![2](https://github.com/hinderss/PPOIS_LABS/assets/111983708/2f269779-28dd-4150-b894-d00b66be5d20)


- Назначение детективов на дела: Игрок может выбрать одного или нескольких детективов для расследования дел.

![3](https://github.com/hinderss/PPOIS_LABS/assets/111983708/70a4ae16-0642-42db-8431-d1fc70696438)


- Назначение офицеров на происшествие: Игрок может выбрать одного или нескольких офицеров для реагирования на происшествие.

![4](https://github.com/hinderss/PPOIS_LABS/assets/111983708/1987a379-f10c-4b5a-b946-23519e0a0a71)


- В консоли изображается текущая смена, которую назначил игрок

![5](https://github.com/hinderss/PPOIS_LABS/assets/111983708/d9849946-a528-476f-be62-d6ceebb88caf)


- На скриншоте показано окно с информацией о происшествии. <br>
Назначение офицеров на происшествие: Игрок может выбрать одного или нескольких офицеров для реагирования на происшествие. <br>
Информация о происшествии: Игрок может посмотреть подробную информацию о происшествии, включая его тип, адрес, время и описание. <br>

![6](https://github.com/hinderss/PPOIS_LABS/assets/111983708/2c3d972f-c5cc-4d0b-ac1e-2b2d4930fe2b)


- На скриншоте показано окно с информацией о происшествии. <br>
Назначение офицеров на происшествие: Игрок может выбрать одного или нескольких офицеров для реагирования на происшествие. <br>

![7](https://github.com/hinderss/PPOIS_LABS/assets/111983708/51d2d819-f1f7-4cc9-abd7-05b9472f9a7a)


- После окончания смены в консоль выводится счёт. За ответ на вызовы начислялись очки, за игнорирование - вычитались.

![8](https://github.com/hinderss/PPOIS_LABS/assets/111983708/86c69eb2-3972-451f-9fec-2609b12b746b)


- После этого можно выйти из программы или начать новую смену.
<br><br>
Перед началом новой смены отображается список дел, которые расследуются в данный момент.

![9](https://github.com/hinderss/PPOIS_LABS/assets/111983708/b85906b6-359f-4ed5-addd-1de690139821)



## Тестирование:
Программа успешно прошла unit-тестирование:

![img_1](https://github.com/hinderss/PPOIS_LABS/assets/111983708/781cf61b-f218-45dc-ae47-637663a63cbf)



## Вывод:
В результате выполнения лабораторной работы были изучены ключевые возможности языка Python для создания программ с интерфейсом командной строки. Затем была успешно разработана программная система на Python, моделирующая функциональность милиции. Это позволило понять применимость Python для разработки эффективных и удобных CLI-приложений и создать работоспособную систему для управления задачами и данными в рамках модели милиции.
