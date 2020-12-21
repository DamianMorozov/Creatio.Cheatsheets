// ------------------------------------------------------------------------------------------------------------------------
// Field validator / Добавление валидации к полю страницы
// https://academy.terrasoft.ru/docs/7-17/developer/front-end_development/creatio_development_cases/page_configuration/dobavlenie_validacii_k_polyu_stranicy
// ------------------------------------------------------------------------------------------------------------------------

methods: {
			onEntityInitialized: function() {
				this.callParent(arguments);
			},
			// Переопределение базового метода, инициализирующего пользовательские валидаторы.
			setValidationConfig: function() {
				// Вызывает инициализацию валидаторов родительской модели представления.
				this.callParent(arguments);
				// Для колонки добавляется метод-валидатор dueDateValidator().
				this.addColumnValidator("StartDate", this.checkDates);
				this.addColumnValidator("EndDate", this.checkDates);
			},
			checkDates: function(value, column) {
				let msg = "";
				if (this.$StartDate && this.$EndDate) {
					if (this.$StartDate > this.$EndDate) {
						msg = this.get("Resources.Strings.CheckDates");
					}
				}
				return {
					invalidMessage: msg
				};
			},
		},
