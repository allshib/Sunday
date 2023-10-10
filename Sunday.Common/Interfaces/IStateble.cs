using DevExpress.Pdf;
using Sunday.Common.BusinessObjects.NonPersistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Common.Interfaces
{
    public interface IStateble
    {

        delegate void StateCheckHandler(StateCheckResult stateCheckResult);

        event StateCheckHandler CheckFinished;


        delegate void AllStateCheckHandler(FinalStateCheckResult finalStateCheckResult);

        event AllStateCheckHandler AllChecksFinished;

        /// <summary>
        /// Текущее состояние объекта
        /// </summary>
        IState CurrentState { get; }
        /// <summary>
        /// Проверить возможность перехода к другому состоянию
        /// </summary>
        /// <param name="newState"></param>
        /// <returns></returns>

        FinalStateCheckResult CheckTransTo(IState newState);

        /// <summary>
        /// Проверяет есть ли такое состояние для последующего перехода
        /// </summary>
        /// <param name="newState"></param>
        /// <returns>Возвращает найденное состояние</returns>
        IState SearchEndpointState(IState newState);


        /// <summary>
        /// Устанавливает новое состояние без ограничений
        /// </summary>
        /// <param name="newState"></param>
        public void SetStateWithoutChecks(IState newState);
    }
}
