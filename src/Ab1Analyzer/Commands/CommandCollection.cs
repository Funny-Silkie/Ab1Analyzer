using System;
using System.Collections.Generic;
using System.Linq;

namespace Ab1Analyzer
{
    /// <summary>
    /// コマンドのコレクションのクラスです。
    /// </summary>
    public class CommandCollection : SortedList<Type, CommandBase>
    {
        /// <summary>
        /// <see cref="CommandCollection"/>の新しいインスタンスを初期化します。
        /// </summary>
        public CommandCollection() : base()
        {
        }

        /// <summary>
        /// <see cref="CommandCollection"/>の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="comparer">キーの比較を行うオブジェクト</param>
        public CommandCollection(IComparer<Type> comparer) : base(comparer)
        {
        }

        /// <summary>
        /// コマンドを追加します。
        /// </summary>
        /// <param name="commandType">コマンドの型</param>
        /// <exception cref="ArgumentException"><paramref name="commandType"/>が既に存在する --または-- <paramref name="commandType"/>で指定される型が<see cref="CommandBase"/>を継承していない</exception>
        /// <exception cref="ArgumentNullException"><paramref name="commandType"/>がnull</exception>
        /// <exception cref="MemberAccessException"><paramref name="commandType"/>の指定する型が抽象クラス</exception>
        /// <exception cref="MissingMethodException"><paramref name="commandType"/>の指定する型がpublicの引数無しコンストラクタを持たない</exception>
        /// <exception cref="System.Reflection.TargetInvocationException">インスタンス生成中に例外が発生</exception>
        public void Add(Type commandType)
        {
            if (commandType == null) throw new ArgumentNullException(nameof(commandType));
            CommandBase command;
            try
            {
                command = (CommandBase)Activator.CreateInstance(commandType);
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException("コマンドの型ではありません", nameof(commandType), e);
            }
            Add(commandType, command);
        }

        /// <summary>
        /// コマンドを取得します。
        /// </summary>
        /// <typeparam name="T">取得するコマンドの型</typeparam>
        /// <returns>コマンド</returns>
        public T GetCommand<T>() where T : CommandBase => (T)this[typeof(T)];

        /// <summary>
        /// 名前からコマンドを取得します。
        /// </summary>
        /// <param name="name">検索するコマンド名</param>
        /// <returns><paramref name="name"/>を持つコマンド 見つからなかったらnull</returns>
        public CommandBase FromName(string name) => Values.SingleOrDefault(x => x.Name == name);
    }
}
